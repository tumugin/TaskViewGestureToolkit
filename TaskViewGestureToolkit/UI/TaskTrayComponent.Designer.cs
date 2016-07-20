namespace TaskViewGestureToolkit.UI
{
    partial class TaskTrayComponent
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.nIconContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.reloadPluginMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitApplicationMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.useAPIStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.nIconContextMenuStrip.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.nIconContextMenuStrip;
            this.notifyIcon.Text = "TaskViewGestureToolkit";
            this.notifyIcon.Visible = true;
            // 
            // nIconContextMenuStrip
            // 
            this.nIconContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reloadPluginMenuItem,
            this.useAPIStripMenuItem,
            this.toolStripSeparator,
            this.exitApplicationMenuItem});
            this.nIconContextMenuStrip.Name = "nIconContextMenuStrip";
            this.nIconContextMenuStrip.Size = new System.Drawing.Size(211, 70);
            // 
            // reloadPluginMenuItem
            // 
            this.reloadPluginMenuItem.Name = "reloadPluginMenuItem";
            this.reloadPluginMenuItem.Size = new System.Drawing.Size(210, 22);
            this.reloadPluginMenuItem.Text = "Reload plugins";
            // 
            // exitApplicationMenuItem
            // 
            this.exitApplicationMenuItem.Name = "exitApplicationMenuItem";
            this.exitApplicationMenuItem.Size = new System.Drawing.Size(210, 22);
            this.exitApplicationMenuItem.Text = "Exit(&E)";
            // 
            // useAPIStripMenuItem
            // 
            this.useAPIStripMenuItem.CheckOnClick = true;
            this.useAPIStripMenuItem.Name = "useAPIStripMenuItem";
            this.useAPIStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.useAPIStripMenuItem.Text = "Use API to switch desktop";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 6);
            this.nIconContextMenuStrip.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.NotifyIcon notifyIcon;
        public System.Windows.Forms.ContextMenuStrip nIconContextMenuStrip;
        public System.Windows.Forms.ToolStripMenuItem reloadPluginMenuItem;
        public System.Windows.Forms.ToolStripMenuItem exitApplicationMenuItem;
        public System.Windows.Forms.ToolStripMenuItem useAPIStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
    }
}
