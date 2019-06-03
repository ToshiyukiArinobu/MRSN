using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using KyoeiSystem.Framework.Common;

namespace KyoeiSystem.Framework.Reports.Preview
{
	/// <summary>
	/// デザイナ用レポートオブジェクト
	/// </summary>
	public partial class RptObjectLabel : UserControl
	{
		#region 定数

		// デザインエリアのフォントの拡大率
		const double fontSizeScale = 19.2;
		// デザインエリアの座標系縮小率
		const double drawScaleX = 0.069;
		const double drawScaleY = 0.073;

		#endregion

		AppLogger appLog = null;
		ReportObject rptItem = null;

		public ObjectInfoType ObjectType = ObjectInfoType.Null;

		public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
					"Text",
					typeof(object),
					typeof(RptObjectLabel),
					new UIPropertyMetadata(string.Empty)
			);

		[BindableAttribute(true)]
		public object Text
		{
			get { return (object)GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}

		public TextDecorationCollection TextDecorations
		{
			get { return this.cText.TextDecorations; }
			set { this.cText.TextDecorations = value; }
		}

		#region TextBlock Property

		#region Content
		/// <summary>
		/// ContentControl のコンテンツを取得または設定します。
		/// </summary>
		[Category("レイアウト")]
		public new object Content
		{
			get { return this.Text; }
			set { this.Text = value; }
		}
		#endregion

		#region HorizontalContentAlignment
		/// <summary>
		/// コントロールのコンテンツの水平方向の配置を取得または設定します
		/// </summary>
		[Category("表示")]
		public new HorizontalAlignment HorizontalContentAlignment
		{
			get
			{
				switch (cText.TextAlignment)
				{
				case TextAlignment.Center:
					return HorizontalAlignment.Center;
				case TextAlignment.Left:
					return HorizontalAlignment.Left;
				case TextAlignment.Right:
					return HorizontalAlignment.Right;
				case TextAlignment.Justify:
					return HorizontalAlignment.Stretch;
				default:
					return HorizontalAlignment.Left;
				}
			}
			set
			{
				switch (value)
				{
				case HorizontalAlignment.Center:
					cText.TextAlignment = TextAlignment.Center;
					break;
				case HorizontalAlignment.Left:
					cText.TextAlignment = TextAlignment.Left;
					break;
				case HorizontalAlignment.Right:
					cText.TextAlignment = TextAlignment.Right;
					break;
				case HorizontalAlignment.Stretch:
					cText.TextAlignment = TextAlignment.Justify;
					break;
				default:
					cText.TextAlignment = TextAlignment.Left;
					break;
				}
			}
		}
		#endregion

		private Brush _frameColor = Brushes.Black;
		public Brush FrameColor
		{
			get { return this._frameColor; }
			set { this._frameColor = value; }
		}

		#endregion


		/// <summary>
		/// 初期化
		/// </summary>
		public RptObjectLabel()
		{
			InitializeComponent();
		}

		public RptObjectLabel(ReportObject item, SectInfo[] sectInfos, ObjectInfo oinfo, AppLogger logger)
		{
			InitializeComponent();
			this.appLog = logger;
			this.rptItem = item;
			initializeItem(item, sectInfos, oinfo);
		}

		private void initializeItem(ReportObject item, SectInfo[] sectInfos, ObjectInfo oinfo)
		{
			try
			{
				var sinfo = (from s in sectInfos where s.name == oinfo.sectionname select s).First();
				double offset = sinfo.offset;
				bool visible = !(item.ObjectFormat.EnableSuppress);
				switch (item.Kind)
				{
				case CrystalDecisions.Shared.ReportObjectKind.FieldObject:
					{
						FieldObject fld = item as FieldObject;
						this.ObjectType = ObjectInfoType.Label;
						//this.cBox.Visibility = System.Windows.Visibility.Collapsed;
						this.cText.Visibility = System.Windows.Visibility.Visible;
						this.Name = fld.Name;
						this.Width = item.Width;
						this.Height = item.Height;
						this.Content = string.Format("{{{0}}}", fld.Name);
						if (visible)
							this.Foreground = new SolidColorBrush(Color.FromArgb(fld.Color.A, fld.Color.R, fld.Color.G, fld.Color.B));
						else
							this.Foreground = Brushes.LightGray;
						this.FontFamily = new System.Windows.Media.FontFamily(fld.Font.FontFamily.Name);
						this.FontSize = fld.Font.Size * fontSizeScale;
						this.FontStretch = FontStretches.ExtraExpanded;
						switch (fld.ObjectFormat.HorizontalAlignment)
						{
						case Alignment.HorizontalCenterAlign:
							this.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
							break;
						case Alignment.LeftAlign:
							this.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;
							break;
						case Alignment.RightAlign:
						case Alignment.Decimal:
							this.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
							break;
						case Alignment.Justified:
							this.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch;
							break;
						default:
							switch (fld.DataSource.ValueType)
							{
							default:
								this.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;
								break;
							case FieldValueType.CurrencyField:
							case FieldValueType.Int32sField:
							case FieldValueType.Int32uField:
							case FieldValueType.Int16sField:
							case FieldValueType.Int16uField:
							case FieldValueType.NumberField:
							case FieldValueType.Int8sField:
							case FieldValueType.Int8uField:
								this.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
								break;
							}
							break;
						}
						this.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
						this.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

						this.TextDecorations = fld.Font.Style.HasFlag(System.Drawing.FontStyle.Underline) ? System.Windows.TextDecorations.Underline : null;
						this.FontWeight = fld.Font.Style.HasFlag(System.Drawing.FontStyle.Bold) ? FontWeights.Bold : FontWeights.Normal;
						this.FontStyle = fld.Font.Style.HasFlag(System.Drawing.FontStyle.Italic) ? FontStyles.Italic : FontStyles.Normal;

						DrawFieldFrameBorder(fld);

						this.SetValue(Canvas.TopProperty, (double)((fld.Top) + offset));
						this.SetValue(Canvas.LeftProperty, (double)(fld.Left));
					}
					break;
				case CrystalDecisions.Shared.ReportObjectKind.FieldHeadingObject:
					{
						FieldHeadingObject fld = item as FieldHeadingObject;
						//this.cBox.Visibility = System.Windows.Visibility.Collapsed;
						this.cText.Visibility = System.Windows.Visibility.Visible;
						this.ObjectType = ObjectInfoType.Label;
						this.Name = fld.Name;
						this.Width = item.Width;
						this.Height = item.Height;
						this.Content = fld.Text;
						if (visible)
							this.Foreground = new SolidColorBrush(Color.FromArgb(fld.Color.A, fld.Color.R, fld.Color.G, fld.Color.B));
						else
							this.Foreground = Brushes.LightGray;
						this.FontFamily = new System.Windows.Media.FontFamily(fld.Font.FontFamily.Name);
						this.FontSize = fld.Font.Size * fontSizeScale;
						this.FontStretch = FontStretches.ExtraExpanded;
						switch (fld.ObjectFormat.HorizontalAlignment)
						{
						case Alignment.HorizontalCenterAlign:
							this.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
							break;
						case Alignment.LeftAlign:
							this.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;
							break;
						case Alignment.RightAlign:
							this.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
							break;
						case Alignment.Justified:
							this.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch;
							break;
						}
						this.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
						this.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

						this.TextDecorations = fld.Font.Style.HasFlag(System.Drawing.FontStyle.Underline) ? System.Windows.TextDecorations.Underline : null;
						this.FontWeight = fld.Font.Style.HasFlag(System.Drawing.FontStyle.Bold) ? FontWeights.Bold : FontWeights.Normal;
						this.FontStyle = fld.Font.Style.HasFlag(System.Drawing.FontStyle.Italic) ? FontStyles.Italic : FontStyles.Normal;

						DrawFieldFrameBorder(fld);

						this.SetValue(Canvas.TopProperty, (double)((fld.Top) + offset));
						this.SetValue(Canvas.LeftProperty, (double)(fld.Left));
					}
					break;
				case CrystalDecisions.Shared.ReportObjectKind.TextObject:
					{
						TextObject fld = item as TextObject;
						//this.cBox.Visibility = System.Windows.Visibility.Collapsed;
						this.cText.Visibility = System.Windows.Visibility.Visible;
						this.ObjectType = ObjectInfoType.Label;
						this.Name = fld.Name;
						this.Width = item.Width;
						this.Height = item.Height;
						this.Content = fld.Text;
						if (visible)
							this.Foreground = new SolidColorBrush(Color.FromArgb(fld.Color.A, fld.Color.R, fld.Color.G, fld.Color.B));
						else
							this.Foreground = Brushes.LightGray;
						this.FontFamily = new System.Windows.Media.FontFamily(fld.Font.FontFamily.Name);
						this.FontSize = fld.Font.Size * fontSizeScale;
						this.FontStretch = FontStretches.ExtraExpanded;
						switch (fld.ObjectFormat.HorizontalAlignment)
						{
						case Alignment.HorizontalCenterAlign:
							this.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
							break;
						case Alignment.LeftAlign:
							this.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;
							break;
						case Alignment.RightAlign:
						case Alignment.Decimal:
							this.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
							break;
						case Alignment.Justified:
							this.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch;
							break;
						}
						this.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
						this.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

						this.TextDecorations = fld.Font.Style.HasFlag(System.Drawing.FontStyle.Underline) ? System.Windows.TextDecorations.Underline : null;
						this.FontWeight = fld.Font.Style.HasFlag(System.Drawing.FontStyle.Bold) ? FontWeights.Bold : FontWeights.Normal;
						this.FontStyle = fld.Font.Style.HasFlag(System.Drawing.FontStyle.Italic) ? FontStyles.Italic : FontStyles.Normal;

						DrawFieldFrameBorder(fld);

						this.SetValue(Canvas.TopProperty, (double)((fld.Top) + offset));
						this.SetValue(Canvas.LeftProperty, (double)(fld.Left));
					}
					break;
				case CrystalDecisions.Shared.ReportObjectKind.BoxObject:
					{
						this.FrameT.Visibility = System.Windows.Visibility.Hidden;
						this.FrameB.Visibility = System.Windows.Visibility.Hidden;
						this.FrameL.Visibility = System.Windows.Visibility.Hidden;
						this.FrameR.Visibility = System.Windows.Visibility.Hidden;

						BoxObject fld = item as BoxObject;
						this.cBox.Visibility = System.Windows.Visibility.Visible;
						this.ObjectType = ObjectInfoType.Box;
						this.Name = fld.Name;
						System.Windows.Shapes.Rectangle box = this.cBox;
						box.Fill = Brushes.Transparent;
						if (visible)
							box.Stroke = new SolidColorBrush(Color.FromArgb(fld.Border.BorderColor.A, fld.Border.BorderColor.R, fld.Border.BorderColor.G, fld.Border.BorderColor.B));
						else
							box.Stroke = Brushes.LightGray;
						box.StrokeThickness = fld.LineThickness;
						this.Width = box.Width = fld.Right - fld.Left;
						double top = fld.Top + offset;
						double ht = fld.Top;
						if (fld.EndSectionName == oinfo.sectionname)
						{
							ht = fld.Bottom - fld.Top;
						}
						else
						{
							var ofs = (from s in sectInfos where s.name == fld.EndSectionName select s.offset).First();
							double bottom = fld.Bottom + ofs;
							ht = bottom - top;
						}
						this.Height = box.Height = ht;
						if (fld.Name.Contains("枠線"))
						{
							try
							{
								// 角丸の値はCOMオブジェクトから取得する方法しかない
								var bf = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
								MethodInfo mi = fld.ObjectFormat.GetType().GetMethod("get_RasReportObject", bf);
								ParameterInfo[] plist = mi.GetParameters();
								object ret = mi.Invoke(fld.ObjectFormat, new object[] { });
								// レポート定義側の値の半分で表示する
								box.RadiusY = ((CrystalDecisions.ReportAppServer.ReportDefModel.BoxObject)ret).CornerEllipseHeight / 2;
								box.RadiusX = ((CrystalDecisions.ReportAppServer.ReportDefModel.BoxObject)ret).CornerEllipseWidth / 2;
							}
							catch (Exception ex)
							{
								appLog.Error("枠線コーナー丸め情報取得エラー", ex);
							}
						}
						this.SetValue(Canvas.TopProperty, top);
						this.SetValue(Canvas.LeftProperty, (double)(fld.Left));
						oinfo.extendSection = fld.EndSectionName != oinfo.sectionname;
						oinfo.endSectionName = fld.EndSectionName;
					}
					break;
				case CrystalDecisions.Shared.ReportObjectKind.LineObject:
					{
						this.FrameT.Visibility = System.Windows.Visibility.Hidden;
						this.FrameB.Visibility = System.Windows.Visibility.Hidden;
						this.FrameL.Visibility = System.Windows.Visibility.Hidden;
						this.FrameR.Visibility = System.Windows.Visibility.Hidden;

						LineObject fld = item as LineObject;
						this.cBox.Visibility = System.Windows.Visibility.Visible;
						this.ObjectType = (fld.Top == fld.Bottom) ? ObjectInfoType.HLine : ObjectInfoType.VLine;
						this.Name = fld.Name;
						System.Windows.Shapes.Rectangle box = this.cBox;
						if (visible)
							box.Stroke = new SolidColorBrush(Color.FromArgb(fld.Border.BorderColor.A, fld.Border.BorderColor.R, fld.Border.BorderColor.G, fld.Border.BorderColor.B));
						else
							box.Stroke = Brushes.LightGray;
						box.Fill = box.Stroke;
						box.StrokeThickness = fld.LineThickness + 5;
						this.Width = box.Width = fld.Right - fld.Left + fld.LineThickness;
						if (oinfo.objectType == ObjectInfoType.VLine)
							this.Width += 5;
						double top = fld.Top + offset;
						double ht = fld.Top;
						if (fld.EndSectionName == oinfo.sectionname)
						{
							ht = fld.Bottom - fld.Top;
						}
						else
						{
							var ofs = (from s in sectInfos where s.name == fld.EndSectionName select s.offset).First();
							double bottom = fld.Bottom + ofs;
							ht = bottom - top;
						}
						this.Height = box.Height = ht + fld.LineThickness;
						if (oinfo.objectType == ObjectInfoType.HLine)
							this.Height += 5;

						this.SetValue(Canvas.TopProperty, top);
						this.SetValue(Canvas.LeftProperty, (double)(fld.Left));
						oinfo.extendSection = fld.EndSectionName != oinfo.sectionname;
						oinfo.endSectionName = fld.EndSectionName;
					}
					break;
                // 追加
                case CrystalDecisions.Shared.ReportObjectKind.BlobFieldObject:
                    {
                        BlobFieldObject Blob = item as BlobFieldObject;
                        this.cText.Visibility = System.Windows.Visibility.Collapsed;
                        this.ObjectType = ObjectInfoType.Label;
                        this.Name = Blob.Name;
                        this.Width = Blob.Width;
                        this.Height = Blob.Height;
                        this.Foreground = Brushes.LightGray;

                        DrawFieldFrameBorder(Blob);

                        this.SetValue(Canvas.TopProperty, (double)((Blob.Top) + offset));
                        this.SetValue(Canvas.LeftProperty, (double)(Blob.Left));
                    }
                    break;
                default:
					break;
				}
			}
			catch (Exception ex)
			{
			}
		}

		private void DrawFieldFrameBorder(ReportObject robj)
		{
			Brush brush = new SolidColorBrush(Color.FromArgb(robj.Border.BorderColor.A, robj.Border.BorderColor.R, robj.Border.BorderColor.G, robj.Border.BorderColor.B));
			drawLine(this.FrameL, robj.Border.LeftLineStyle, brush);
			drawLine(this.FrameR, robj.Border.RightLineStyle, brush);
			this.FrameR.X1 = this.FrameR.X2 = this.Width - (this.FrameR.StrokeThickness / 2);
			drawLine(this.FrameT, robj.Border.TopLineStyle, brush);
			drawLine(this.FrameB, robj.Border.BottomLineStyle, brush);
			this.FrameB.Y1 = this.FrameB.Y2 = this.Height - (this.FrameB.StrokeThickness / 2);
		}

		void drawLine(Line line, LineStyle lstyle, Brush brush)
		{
			switch (lstyle)
			{
			case LineStyle.SingleLine:
				line.Visibility = System.Windows.Visibility.Visible;
				line.Stroke = brush;
				break;
			case LineStyle.DoubleLine:
				line.Visibility = System.Windows.Visibility.Visible;
				line.Stroke = brush;
				break;
			case LineStyle.DotLine:
				line.Visibility = System.Windows.Visibility.Visible;
				line.Stroke = brush;
				line.StrokeDashArray = new DoubleCollection() { 1, 1, };
				line.StrokeDashOffset = 0;
				break;
			case LineStyle.DashLine:
				line.Visibility = System.Windows.Visibility.Visible;
				line.Stroke = brush;
				line.StrokeDashArray = new DoubleCollection() { 3, 1, };
				line.StrokeDashOffset = 0;
				break;
			default:
				line.Visibility = System.Windows.Visibility.Hidden;
				break;
			}
		}
	}
}
