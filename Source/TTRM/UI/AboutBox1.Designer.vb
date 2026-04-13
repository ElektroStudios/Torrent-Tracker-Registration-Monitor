Imports DarkUI.Controls
Imports DarkUI.Forms

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AboutBox1
    Inherits DarkForm

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

    Friend WithEvents TableLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LogoPictureBox As System.Windows.Forms.PictureBox
    Friend WithEvents LabelProductName As System.Windows.Forms.Label
    Friend WithEvents LabelVersion As System.Windows.Forms.Label
    Friend WithEvents LinkLabelGitHub As System.Windows.Forms.LinkLabel
    Friend WithEvents TextBoxDescription As DarkTextBox
    Friend WithEvents OKButton As DarkButton
    Friend WithEvents LabelCopyright As System.Windows.Forms.Label

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AboutBox1))
        Me.TableLayoutPanel = New TableLayoutPanel()
        Me.LogoPictureBox = New PictureBox()
        Me.LabelProductName = New Label()
        Me.LabelVersion = New Label()
        Me.LabelCopyright = New Label()
        Me.LinkLabelGitHub = New LinkLabel()
        Me.TextBoxDescription = New DarkTextBox()
        Me.OKButton = New DarkButton()
        Me.TableLayoutPanel.SuspendLayout()
        CType(Me.LogoPictureBox, ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        ' 
        ' TableLayoutPanel
        ' 
        Me.TableLayoutPanel.ColumnCount = 2
        Me.TableLayoutPanel.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 22.90076F))
        Me.TableLayoutPanel.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 77.09924F))
        Me.TableLayoutPanel.Controls.Add(Me.LogoPictureBox, 0, 0)
        Me.TableLayoutPanel.Controls.Add(Me.LabelProductName, 1, 0)
        Me.TableLayoutPanel.Controls.Add(Me.LabelVersion, 1, 1)
        Me.TableLayoutPanel.Controls.Add(Me.LabelCopyright, 1, 2)
        Me.TableLayoutPanel.Controls.Add(Me.LinkLabelGitHub, 1, 3)
        Me.TableLayoutPanel.Controls.Add(Me.TextBoxDescription, 1, 4)
        Me.TableLayoutPanel.Controls.Add(Me.OKButton, 1, 5)
        Me.TableLayoutPanel.Dock = DockStyle.Fill
        Me.TableLayoutPanel.Location = New Point(10, 11)
        Me.TableLayoutPanel.Margin = New Padding(4, 3, 4, 3)
        Me.TableLayoutPanel.Name = "TableLayoutPanel"
        Me.TableLayoutPanel.RowCount = 6
        Me.TableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 10F))
        Me.TableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 10F))
        Me.TableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 10F))
        Me.TableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 10F))
        Me.TableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        Me.TableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 10F))
        Me.TableLayoutPanel.Size = New Size(524, 339)
        Me.TableLayoutPanel.TabIndex = 0
        ' 
        ' LogoPictureBox
        ' 
        Me.LogoPictureBox.BackColor = Color.FromArgb(CByte(44), CByte(44), CByte(44))
        Me.LogoPictureBox.BackgroundImage = My.Resources.Resources.Elektro_Banner___Classic
        Me.LogoPictureBox.BackgroundImageLayout = ImageLayout.Zoom
        Me.LogoPictureBox.Dock = DockStyle.Fill
        Me.LogoPictureBox.Location = New Point(4, 3)
        Me.LogoPictureBox.Margin = New Padding(4, 3, 4, 3)
        Me.LogoPictureBox.Name = "LogoPictureBox"
        Me.TableLayoutPanel.SetRowSpan(Me.LogoPictureBox, 6)
        Me.LogoPictureBox.Size = New Size(111, 333)
        Me.LogoPictureBox.SizeMode = PictureBoxSizeMode.StretchImage
        Me.LogoPictureBox.TabIndex = 0
        Me.LogoPictureBox.TabStop = False
        ' 
        ' LabelProductName
        ' 
        Me.LabelProductName.Dock = DockStyle.Fill
        Me.LabelProductName.Font = New Font("Segoe UI", 11F)
        Me.LabelProductName.ForeColor = Color.Gainsboro
        Me.LabelProductName.Location = New Point(126, 0)
        Me.LabelProductName.Margin = New Padding(7, 0, 4, 0)
        Me.LabelProductName.MaximumSize = New Size(0, 23)
        Me.LabelProductName.Name = "LabelProductName"
        Me.LabelProductName.Size = New Size(394, 23)
        Me.LabelProductName.TabIndex = 1
        Me.LabelProductName.Text = "Product Name"
        Me.LabelProductName.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' LabelVersion
        ' 
        Me.LabelVersion.Dock = DockStyle.Fill
        Me.LabelVersion.Font = New Font("Segoe UI", 11F)
        Me.LabelVersion.ForeColor = Color.Gainsboro
        Me.LabelVersion.Location = New Point(126, 33)
        Me.LabelVersion.Margin = New Padding(7, 0, 4, 0)
        Me.LabelVersion.MaximumSize = New Size(0, 23)
        Me.LabelVersion.Name = "LabelVersion"
        Me.LabelVersion.Size = New Size(394, 23)
        Me.LabelVersion.TabIndex = 2
        Me.LabelVersion.Text = "Version"
        Me.LabelVersion.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' LabelCopyright
        ' 
        Me.LabelCopyright.Dock = DockStyle.Fill
        Me.LabelCopyright.Font = New Font("Segoe UI", 11F)
        Me.LabelCopyright.ForeColor = Color.Gainsboro
        Me.LabelCopyright.Location = New Point(126, 66)
        Me.LabelCopyright.Margin = New Padding(7, 0, 4, 0)
        Me.LabelCopyright.MaximumSize = New Size(0, 23)
        Me.LabelCopyright.Name = "LabelCopyright"
        Me.LabelCopyright.Size = New Size(394, 23)
        Me.LabelCopyright.TabIndex = 3
        Me.LabelCopyright.Text = "Copyright"
        Me.LabelCopyright.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' LinkLabelGitHub
        ' 
        Me.LinkLabelGitHub.Dock = DockStyle.Fill
        Me.LinkLabelGitHub.Font = New Font("Segoe UI", 11F)
        Me.LinkLabelGitHub.ForeColor = Color.Gainsboro
        Me.LinkLabelGitHub.LinkColor = SystemColors.ActiveCaption
        Me.LinkLabelGitHub.Location = New Point(126, 99)
        Me.LinkLabelGitHub.Margin = New Padding(7, 0, 4, 0)
        Me.LinkLabelGitHub.MaximumSize = New Size(0, 23)
        Me.LinkLabelGitHub.Name = "LinkLabelGitHub"
        Me.LinkLabelGitHub.Size = New Size(394, 23)
        Me.LinkLabelGitHub.TabIndex = 4
        Me.LinkLabelGitHub.TabStop = True
        Me.LinkLabelGitHub.Text = "GitHub"
        Me.LinkLabelGitHub.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' TextBoxDescription
        ' 
        Me.TextBoxDescription.BackColor = Color.FromArgb(CByte(69), CByte(73), CByte(74))
        Me.TextBoxDescription.BorderStyle = BorderStyle.FixedSingle
        Me.TextBoxDescription.Dock = DockStyle.Fill
        Me.TextBoxDescription.Font = New Font("Segoe UI", 11F)
        Me.TextBoxDescription.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        Me.TextBoxDescription.Location = New Point(126, 135)
        Me.TextBoxDescription.Margin = New Padding(7, 3, 4, 3)
        Me.TextBoxDescription.Multiline = True
        Me.TextBoxDescription.Name = "TextBoxDescription"
        Me.TextBoxDescription.ReadOnly = True
        Me.TextBoxDescription.ScrollBars = ScrollBars.Both
        Me.TextBoxDescription.Size = New Size(394, 163)
        Me.TextBoxDescription.TabIndex = 5
        Me.TextBoxDescription.TabStop = False
        Me.TextBoxDescription.Text = resources.GetString("TextBoxDescription.Text")
        ' 
        ' OKButton
        ' 
        Me.OKButton.DialogResult = DialogResult.Cancel
        Me.OKButton.Dock = DockStyle.Right
        Me.OKButton.Font = New Font("Segoe UI", 11F)
        Me.OKButton.Location = New Point(432, 304)
        Me.OKButton.Margin = New Padding(4, 3, 4, 3)
        Me.OKButton.Name = "OKButton"
        Me.OKButton.Padding = New Padding(5, 6, 5, 6)
        Me.OKButton.Size = New Size(88, 32)
        Me.OKButton.TabIndex = 0
        Me.OKButton.Text = "OK"
        ' 
        ' AboutBox1
        ' 
        Me.AutoScaleDimensions = New SizeF(7F, 17F)
        Me.AutoScaleMode = AutoScaleMode.Font
        Me.CancelButton = Me.OKButton
        Me.ClientSize = New Size(544, 361)
        Me.Controls.Add(Me.TableLayoutPanel)
        Me.Font = New Font("Segoe UI", 10F)
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.Margin = New Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AboutBox1"
        Me.Padding = New Padding(10, 11, 10, 11)
        Me.ShowInTaskbar = False
        Me.StartPosition = FormStartPosition.CenterParent
        Me.Text = "AboutBox1"
        Me.TableLayoutPanel.ResumeLayout(False)
        Me.TableLayoutPanel.PerformLayout()
        CType(Me.LogoPictureBox, ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

End Class
