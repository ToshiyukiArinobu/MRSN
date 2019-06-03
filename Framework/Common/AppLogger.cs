using System;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Resources;
using System.Reflection;
using System.Threading;
using System.Linq;
using System.Collections.Generic;
using log4net;

namespace KyoeiSystem.Framework.Common
{
	/// <summary>
	/// �A�v���P�[�V�������O�N���X
	/// <para>
	/// ���O�̏o�͂����܂Ƃ߂�@�\�Ƃ��ė��p�ł��܂��B�ڍׂ�log4net(NuGet����擾�����p�b�P�[�W)�̋@�\���Q�Ƃ��Ă��������B
	/// </para>
	/// </summary>
	public class AppLogger
	{
		//NLog.Logger logger = LogManager.GetCurrentClassLogger();
		ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		#region �C���X�^���X�̃V���O���g����
		private static AppLogger mInstance;

		private AppLogger()
		{
		}

		public static AppLogger Instance
		{
			get
			{
				if (mInstance == null) mInstance = new AppLogger();
				return mInstance;
			}
		}
		#endregion
		///// <summary>
		///// �R���X�g���N�^
		///// </summary>
		//public AppLogger()
		//{
		//}

		//~AppLogger()
		//{
		//}

		#region �e���x�����̃��O�o�̓��\�b�h
		/// <summary>
		/// ���O�o�́iFATAL�j
		/// </summary>
		/// <param name="msg">���b�Z�[�W</param>
		public void Fatal(string msg)
		{
			logger.Fatal(msg);
		}

		/// <summary>
		/// ���O�o�́iFATAL�j
		/// </summary>
		/// <param name="msg">���b�Z�[�W</param>
		/// <param name="ex">�p����O</param>
		public void Fatal(string msg, Exception ex)
		{
			logger.Fatal(msg, ex);
		}

		/// <summary>
		/// ���O�o�́iFATAL�j
		/// </summary>
		/// <param name="fmt">�ҏW�t�H�[�}�b�g</param>
		/// <param name="args">�ҏW�p�����[�^</param>
		public void Fatal(string fmt, params object[] args)
		{
			logger.Fatal(string.Format(fmt, args));
		}

		/// <summary>
		/// ���O�o�́iERROR�j
		/// </summary>
		/// <param name="msg">���b�Z�[�W</param>
		public void Error(string msg)
		{
			logger.Error(msg);
		}

		/// <summary>
		/// ���O�o�́iERROR�j
		/// </summary>
		/// <param name="msg">���b�Z�[�W</param>
		/// <param name="ex">�p����O</param>
		public void Error(string msg, Exception ex)
		{
			logger.Error(msg, ex);
		}

		/// <summary>
		/// ���O�o�́iERROR�j
		/// </summary>
		/// <param name="fmt">�ҏW�t�H�[�}�b�g</param>
		/// <param name="args">�ҏW�p�����[�^</param>
		public void Error(string fmt, params object[] args)
		{
			logger.Error(string.Format(fmt, args));
		}

		/// <summary>
		/// ���O�o�́iWARN�j
		/// </summary>
		/// <param name="msg">���b�Z�[�W</param>
		public void Warn(string msg)
		{
			logger.Warn(msg);
		}

		/// <summary>
		/// ���O�o�́iWARN�j
		/// </summary>
		/// <param name="msg">���b�Z�[�W</param>
		/// <param name="ex">�p����O</param>
		public void Warn(string msg, Exception ex)
		{
			logger.Warn(msg, ex);
		}

		/// <summary>
		/// ���O�o�́iWARN�j
		/// </summary>
		/// <param name="fmt">�ҏW�t�H�[�}�b�g</param>
		/// <param name="args">�ҏW�p�����[�^</param>
		public void Warn(string fmt, params object[] args)
		{
			logger.Warn(string.Format(fmt, args));
		}

		/// <summary>
		/// ���O�o�́iINFO�j
		/// </summary>
		/// <param name="msg">���b�Z�[�W</param>
		public void Info(string msg)
		{
			logger.Info(msg);
		}

		/// <summary>
		/// ���O�o�́iINFO�j
		/// </summary>
		/// <param name="msg">���b�Z�[�W</param>
		/// <param name="ex">�p����O</param>
		public void Info(string msg, Exception ex)
		{
			logger.Info(msg, ex);
		}

		/// <summary>
		/// ���O�o�́iINFO�j
		/// </summary>
		/// <param name="fmt">�ҏW�t�H�[�}�b�g</param>
		/// <param name="args">�ҏW�p�����[�^</param>
		public void Info(string fmt, params object[] args)
		{
			logger.Info(string.Format(fmt, args));
		}

		/// <summary>
		/// ���O�o�́iDEBUG�j
		/// </summary>
		/// <param name="msg">���b�Z�[�W</param>
		public void Debug(string msg)
		{
			logger.Debug(msg);
		}

		/// <summary>
		/// ���O�o�́iDEBUG�j
		/// </summary>
		/// <param name="msg">���b�Z�[�W</param>
		/// <param name="ex">�p����O</param>
		public void Debug(string msg, Exception ex)
		{
			logger.Debug(msg, ex);
		}

		/// <summary>
		/// ���O�o�́iDEBUG�j
		/// </summary>
		/// <param name="fmt">�ҏW�t�H�[�}�b�g</param>
		/// <param name="args">�ҏW�p�����[�^</param>
		public void Debug(string fmt, params object[] args)
		{
			logger.Debug(string.Format(fmt, args));
		}
		#endregion

		#region ���O�t�@�C���p�X�̎擾
		/// <summary>
		/// ���O�t�@�C���p�X�擾
		/// </summary>
		/// <returns>���O�t�@�C���p�X</returns>
		public string LogFilePath()
		{
			string path = string.Empty;

			foreach (var appender in logger.Logger.Repository.GetAppenders())
			{
				if (appender is log4net.Appender.FileAppender)
				{
					return (appender as log4net.Appender.FileAppender).File;
				}
			}
			return path;
		}
		#endregion

		#region �ۑ��������߂������O�t�@�C���̍폜
		/// <summary>
		/// �ۑ��������߂������O�t�@�C�����폜���܂��B
		/// <para>Appender�́A���t���Ƀt�@�C������؂�ւ��郂�[�h�ł��邱�Ƃ��O��ł���A���Y�t�H���_�ɂ���t�@�C���S�Ă�ΏۂƂ��č폜���܂��B
		/// �ۑ������́AApp.config���ɋL�q����log4net�̐ݒ��Appender��MaxSizeRollBackups�̒l�ɂ��܂��B</para>
		/// </summary>
		public void CleanUp()
		{
			try
			{
				int keep_days = 1;

				var repo = this.logger.Logger.Repository;
				var app = repo.GetAppenders().Where(x => x.GetType() == typeof(log4net.Appender.RollingFileAppender)).FirstOrDefault();
				if (app != null)
				{
					var appender = app as log4net.Appender.RollingFileAppender;
					if (appender.RollingStyle != log4net.Appender.RollingFileAppender.RollingMode.Date)
					{
						// ���O�t�@�C���ؑփ��[�h���u���t�v�łȂ���Ή������Ȃ�
						return;
					}
					string file = appender.File;
					string directory = Path.GetDirectoryName(file);
					if (string.IsNullOrEmpty(directory))
					{
						return;
					}

					var dirInfo = new DirectoryInfo(directory);
					if (!dirInfo.Exists)
					{
						return;
					}

					// MaxSizeRollBackups ���ő働�O�����Ƃ��đ�p����B
					keep_days = appender.MaxSizeRollBackups;
					if (keep_days <= 0)
					{
						return;
					}
					string limit = DateTime.Now.AddDays(-keep_days).ToString("yyyyMMdd");
					foreach (var info in dirInfo.GetFiles("*"))
					{
						string ymd = info.LastWriteTime.ToString("yyyyMMdd");
						if (string.Compare(ymd, limit) < 0)
						{
							Info("�Â����O���폜 ({0})", info.Name);
							info.Delete();
						}
					}
				}

			}
			catch (Exception ex)
			{
				this.Error("���O�t�@�C���A�[�J�C�u�̍폜�ŃG���[���������܂����B", ex);
			}
		}

		#endregion

	}
}
