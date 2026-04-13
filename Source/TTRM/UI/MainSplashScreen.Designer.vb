<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class MainSplashScreen
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub
    Friend WithEvents ApplicationTitle As System.Windows.Forms.Label
    Friend WithEvents Version As System.Windows.Forms.Label
    Friend WithEvents Copyright As System.Windows.Forms.Label

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Version = New Label()
        Me.Copyright = New Label()
        Me.ApplicationTitle = New Label()
        Me.Label_StatusLoad = New Label()
        Me.SuspendLayout()
        ' 
        ' Version
        ' 
        Me.Version.Anchor = AnchorStyles.None
        Me.Version.BackColor = Color.Transparent
        Me.Version.Font = New Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Me.Version.Location = New Point(13, 443)
        Me.Version.Margin = New Padding(4, 0, 4, 0)
        Me.Version.Name = "Version"
        Me.Version.Size = New Size(147, 28)
        Me.Version.TabIndex = 1
        Me.Version.Text = "Version {0}"
        Me.Version.UseWaitCursor = True
        ' 
        ' Copyright
        ' 
        Me.Copyright.Anchor = AnchorStyles.None
        Me.Copyright.BackColor = Color.Transparent
        Me.Copyright.Font = New Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Me.Copyright.Location = New Point(13, 486)
        Me.Copyright.Margin = New Padding(4, 0, 4, 0)
        Me.Copyright.Name = "Copyright"
        Me.Copyright.Size = New Size(147, 28)
        Me.Copyright.TabIndex = 2
        Me.Copyright.Text = "Copyright"
        Me.Copyright.UseWaitCursor = True
        ' 
        ' ApplicationTitle
        ' 
        Me.ApplicationTitle.BackColor = Color.Transparent
        Me.ApplicationTitle.Dock = DockStyle.Top
        Me.ApplicationTitle.Font = New Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Me.ApplicationTitle.Location = New Point(0, 0)
        Me.ApplicationTitle.Margin = New Padding(4, 0, 4, 0)
        Me.ApplicationTitle.Name = "ApplicationTitle"
        Me.ApplicationTitle.Size = New Size(818, 77)
        Me.ApplicationTitle.TabIndex = 0
        Me.ApplicationTitle.Text = "Application Title"
        Me.ApplicationTitle.TextAlign = ContentAlignment.BottomCenter
        Me.ApplicationTitle.UseWaitCursor = True
        ' 
        ' Label_StatusLoad
        ' 
        Me.Label_StatusLoad.Anchor = AnchorStyles.None
        Me.Label_StatusLoad.BackColor = Color.Transparent
        Me.Label_StatusLoad.Font = New Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Me.Label_StatusLoad.Location = New Point(266, 244)
        Me.Label_StatusLoad.Margin = New Padding(4, 0, 4, 0)
        Me.Label_StatusLoad.Name = "Label_StatusLoad"
        Me.Label_StatusLoad.Size = New Size(171, 132)
        Me.Label_StatusLoad.TabIndex = 3
        Me.Label_StatusLoad.Text = "Initializing application..."
        Me.Label_StatusLoad.TextAlign = ContentAlignment.MiddleCenter
        Me.Label_StatusLoad.UseWaitCursor = True
        ' 
        ' MainSplashScreen
        ' 
        Me.AutoScaleDimensions = New SizeF(9F, 21F)
        Me.AutoScaleMode = AutoScaleMode.Font
        Me.BackgroundImage = My.Resources.Resources.splash
        Me.BackgroundImageLayout = ImageLayout.Stretch
        Me.ClientSize = New Size(818, 528)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label_StatusLoad)
        Me.Controls.Add(Me.Copyright)
        Me.Controls.Add(Me.Version)
        Me.Controls.Add(Me.ApplicationTitle)
        Me.DoubleBuffered = True
        Me.Font = New Font("Segoe UI", 12F)
        Me.ForeColor = Color.WhiteSmoke
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.Margin = New Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "MainSplashScreen"
        Me.ShowInTaskbar = False
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.UseWaitCursor = True
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Label_StatusLoad As Label

End Class
