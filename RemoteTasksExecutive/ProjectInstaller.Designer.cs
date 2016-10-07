namespace RemoteTasksExecutive
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
            this.ExecutiveServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.ExecutiveServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // ExecutiveServiceProcessInstaller
            // 
            this.ExecutiveServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.ExecutiveServiceProcessInstaller.Password = null;
            this.ExecutiveServiceProcessInstaller.Username = null;
            // 
            // ExecutiveServiceInstaller
            // 
            this.ExecutiveServiceInstaller.Description = "Remote Tasks Executive Service";
            this.ExecutiveServiceInstaller.DisplayName = "Remote Tasks Executive Service";
            this.ExecutiveServiceInstaller.ServiceName = "RemoteTasksExecutiveService";
            this.ExecutiveServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.ExecutiveServiceProcessInstaller,
            this.ExecutiveServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller ExecutiveServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller ExecutiveServiceInstaller;
    }
}