namespace CentralServer
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.CentralServerProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.CentralServerInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // CentralServerProcessInstaller
            // 
            this.CentralServerProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.CentralServerProcessInstaller.Password = null;
            this.CentralServerProcessInstaller.Username = null;
            // 
            // CentralServerInstaller
            // 
            this.CentralServerInstaller.Description = "Remote Tasks Central Server";
            this.CentralServerInstaller.DisplayName = "RemoteTasksCentralServer";
            this.CentralServerInstaller.ServiceName = "RemoteTasksCentralServer";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.CentralServerProcessInstaller,
            this.CentralServerInstaller});

        }

        #endregion

        internal System.ServiceProcess.ServiceProcessInstaller CentralServerProcessInstaller;
        private System.ServiceProcess.ServiceInstaller CentralServerInstaller;
    }
}