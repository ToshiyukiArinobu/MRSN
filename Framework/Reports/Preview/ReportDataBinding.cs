using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace KyoeiSystem.Framework.Reports.Preview
{
	public class ReportDataBinding : INotifyPropertyChanged
	{
		public IEnumerable<FontFamily> FontList { get; set; }
	
		private List<TreeNode> _tree;
		public List<TreeNode> Tree
		{
			get
			{
				return this._tree;
			}
			set
			{
				this._tree = value;
				this.NotifyPropertyChanged();
			}
		}

		private List<ComboboxColor> _foreColorList;
		public List<ComboboxColor> ForeColorList
		{
			get
			{
				return this._foreColorList;
			}
			set
			{
				this._foreColorList = value;
				this.NotifyPropertyChanged();
			}
		}
		public ComboboxColor SelectedForeColor { get; set; }
		public void SetSelectedForeColor(string name)
		{
			foreach (ComboboxColor clr in this.ForeColorList)
			{
				if (clr.Text == name)
				{
					this.SelectedForeColor = clr;
					return;
				}
			}
			SelectedForeColor = null;
		}
		private List<ComboboxColor> _frameColorList;
		public List<ComboboxColor> FrameColorList
		{
			get
			{
				return this._frameColorList;
			}
			set
			{
				this._frameColorList = value;
				this.NotifyPropertyChanged();
			}
		}
		public ComboboxColor SelectedFrameColor
		{
			get; set;
		}

		public void SetSelectedFrameColor(string name)
		{
			foreach (ComboboxColor clr in this.FrameColorList)
			{
				if (clr.Text == name)
				{
					this.SelectedFrameColor = clr;
					return;
				}
			}
			SelectedFrameColor = null;
		}

		private bool _buttonBackIsEnabled = false;
		public bool ButtonBackIsEnabled
		{
			get
			{
				return this._buttonBackIsEnabled;
			}
			set
			{
				this._buttonBackIsEnabled = value;
				this.NotifyPropertyChanged();
			}
		}

		public TreeNode FindNode(string name, List<TreeNode> tree = null)
		{
			if (tree == null) { tree = this.Tree; }
			foreach (TreeNode node in tree)
			{
				if (node.Name == name)
				{
					return node;
				}
				TreeNode item = this.FindNode(name, node.Children);
				if (item != null)
				{
					return item;
				}
			}
			return null;
		}

		private int _currentPage = 0;
		public int CurrentPage
		{
			get { return _currentPage; }
			set { _currentPage = value; NotifyPropertyChanged(); }
		}

		private int _lastPage = 0;
		public int LastPage
		{
			get { return _lastPage; }
			set { _lastPage = value; NotifyPropertyChanged(); }
		}



		#region INotifyPropertyChanged メンバー

		public event PropertyChangedEventHandler PropertyChanged;
		private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		#endregion
	}

	public class TreeNode : INotifyPropertyChanged
	{
		public string Name { get; set; }
		public List<TreeNode> Children { get; set; }
		public string SectionName { get; set; }

		// ロード時にのみ使用する座標
		public int X = 0;
		public int Y = 0;

		public bool Visible { get; set; }
		private bool isSelected = false;
		public bool IsSelected
		{
			get { return this.isSelected; }
			set
			{
				if (this.isSelected != value)
				{
					this.isSelected = value;
				}
				NotifyPropertyChanged();
			}
		}
		public TreeNode()
		{
			this.Children = new List<TreeNode>();
			this.Visible = true;
		}

		#region INotifyPropertyChanged メンバー

		public event PropertyChangedEventHandler PropertyChanged;
		private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		#endregion

	}
	//public class RedTreeNode : TreeNode
	//{
	//}
	public class TerminalNode : TreeNode
	{
		public string Detail { get; set; }

		public TerminalNode()
		{
			this.Visible = true;
		}
	}

}
