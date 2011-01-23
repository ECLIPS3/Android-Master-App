<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.btnReboot_Phone = New System.Windows.Forms.Button()
        Me.lblMainHeading = New System.Windows.Forms.Label()
        Me.lblReboot_Options = New System.Windows.Forms.Label()
        Me.btnReboot_Recovery = New System.Windows.Forms.Button()
        Me.btnReboot_Bootloader = New System.Windows.Forms.Button()
        Me.lblRootingPhone = New System.Windows.Forms.Label()
        Me.btnRootStep2 = New System.Windows.Forms.Button()
        Me.btnRootStep3 = New System.Windows.Forms.Button()
        Me.btnRootStep4 = New System.Windows.Forms.Button()
        Me.OpenRomFile = New System.Windows.Forms.OpenFileDialog()
        Me.lblHeading = New System.Windows.Forms.Label()
        Me.lblApplicationManagement = New System.Windows.Forms.Label()
        Me.btnBackupAppsToSDCARD = New System.Windows.Forms.Button()
        Me.btnReinstallAllAppsFromSDCARD = New System.Windows.Forms.Button()
        Me.btnStartDownloads = New System.Windows.Forms.Button()
        Me.chbxROOTImage = New System.Windows.Forms.CheckBox()
        Me.lblROOTRequired = New System.Windows.Forms.Label()
        Me.prgsTestProgressBar = New System.Windows.Forms.ProgressBar()
        Me.lblTotalDownloaded = New System.Windows.Forms.Label()
        Me.chbxRecoveryImage = New System.Windows.Forms.CheckBox()
        Me.grbxROMToInstall = New System.Windows.Forms.GroupBox()
        Me.radCyanLightBoltFroyo = New System.Windows.Forms.RadioButton()
        Me.radCyanLightBolt = New System.Windows.Forms.RadioButton()
        Me.radIceRom = New System.Windows.Forms.RadioButton()
        Me.radEvilEris = New System.Windows.Forms.RadioButton()
        Me.radxtrROM = New System.Windows.Forms.RadioButton()
        Me.radVanillaDroid = New System.Windows.Forms.RadioButton()
        Me.radKaosLegendary = New System.Windows.Forms.RadioButton()
        Me.radErisOfficial = New System.Windows.Forms.RadioButton()
        Me.radSenseable2 = New System.Windows.Forms.RadioButton()
        Me.btnFlashSplashImage = New System.Windows.Forms.Button()
        Me.btnBackupAppSettings = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.grbxROMToInstall.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnReboot_Phone
        '
        resources.ApplyResources(Me.btnReboot_Phone, "btnReboot_Phone")
        Me.btnReboot_Phone.Name = "btnReboot_Phone"
        Me.btnReboot_Phone.UseVisualStyleBackColor = True
        '
        'lblMainHeading
        '
        resources.ApplyResources(Me.lblMainHeading, "lblMainHeading")
        Me.lblMainHeading.Name = "lblMainHeading"
        '
        'lblReboot_Options
        '
        resources.ApplyResources(Me.lblReboot_Options, "lblReboot_Options")
        Me.lblReboot_Options.Name = "lblReboot_Options"
        '
        'btnReboot_Recovery
        '
        resources.ApplyResources(Me.btnReboot_Recovery, "btnReboot_Recovery")
        Me.btnReboot_Recovery.Name = "btnReboot_Recovery"
        Me.btnReboot_Recovery.UseVisualStyleBackColor = True
        '
        'btnReboot_Bootloader
        '
        resources.ApplyResources(Me.btnReboot_Bootloader, "btnReboot_Bootloader")
        Me.btnReboot_Bootloader.Name = "btnReboot_Bootloader"
        Me.btnReboot_Bootloader.UseVisualStyleBackColor = True
        '
        'lblRootingPhone
        '
        resources.ApplyResources(Me.lblRootingPhone, "lblRootingPhone")
        Me.lblRootingPhone.Name = "lblRootingPhone"
        '
        'btnRootStep2
        '
        resources.ApplyResources(Me.btnRootStep2, "btnRootStep2")
        Me.btnRootStep2.Name = "btnRootStep2"
        Me.btnRootStep2.UseVisualStyleBackColor = True
        '
        'btnRootStep3
        '
        resources.ApplyResources(Me.btnRootStep3, "btnRootStep3")
        Me.btnRootStep3.Name = "btnRootStep3"
        Me.btnRootStep3.UseVisualStyleBackColor = True
        '
        'btnRootStep4
        '
        resources.ApplyResources(Me.btnRootStep4, "btnRootStep4")
        Me.btnRootStep4.Name = "btnRootStep4"
        Me.btnRootStep4.UseVisualStyleBackColor = True
        '
        'OpenRomFile
        '
        Me.OpenRomFile.FileName = "*.zip"
        '
        'lblHeading
        '
        resources.ApplyResources(Me.lblHeading, "lblHeading")
        Me.lblHeading.Name = "lblHeading"
        '
        'lblApplicationManagement
        '
        resources.ApplyResources(Me.lblApplicationManagement, "lblApplicationManagement")
        Me.lblApplicationManagement.Name = "lblApplicationManagement"
        '
        'btnBackupAppsToSDCARD
        '
        resources.ApplyResources(Me.btnBackupAppsToSDCARD, "btnBackupAppsToSDCARD")
        Me.btnBackupAppsToSDCARD.Name = "btnBackupAppsToSDCARD"
        Me.btnBackupAppsToSDCARD.UseVisualStyleBackColor = True
        '
        'btnReinstallAllAppsFromSDCARD
        '
        resources.ApplyResources(Me.btnReinstallAllAppsFromSDCARD, "btnReinstallAllAppsFromSDCARD")
        Me.btnReinstallAllAppsFromSDCARD.Name = "btnReinstallAllAppsFromSDCARD"
        Me.btnReinstallAllAppsFromSDCARD.UseVisualStyleBackColor = True
        '
        'btnStartDownloads
        '
        resources.ApplyResources(Me.btnStartDownloads, "btnStartDownloads")
        Me.btnStartDownloads.Name = "btnStartDownloads"
        Me.btnStartDownloads.UseVisualStyleBackColor = True
        '
        'chbxROOTImage
        '
        resources.ApplyResources(Me.chbxROOTImage, "chbxROOTImage")
        Me.chbxROOTImage.Name = "chbxROOTImage"
        Me.chbxROOTImage.UseVisualStyleBackColor = True
        '
        'lblROOTRequired
        '
        resources.ApplyResources(Me.lblROOTRequired, "lblROOTRequired")
        Me.lblROOTRequired.Name = "lblROOTRequired"
        '
        'prgsTestProgressBar
        '
        resources.ApplyResources(Me.prgsTestProgressBar, "prgsTestProgressBar")
        Me.prgsTestProgressBar.Name = "prgsTestProgressBar"
        '
        'lblTotalDownloaded
        '
        resources.ApplyResources(Me.lblTotalDownloaded, "lblTotalDownloaded")
        Me.lblTotalDownloaded.Name = "lblTotalDownloaded"
        '
        'chbxRecoveryImage
        '
        resources.ApplyResources(Me.chbxRecoveryImage, "chbxRecoveryImage")
        Me.chbxRecoveryImage.Name = "chbxRecoveryImage"
        Me.chbxRecoveryImage.UseVisualStyleBackColor = True
        '
        'grbxROMToInstall
        '
        Me.grbxROMToInstall.Controls.Add(Me.radCyanLightBoltFroyo)
        Me.grbxROMToInstall.Controls.Add(Me.radCyanLightBolt)
        Me.grbxROMToInstall.Controls.Add(Me.radIceRom)
        Me.grbxROMToInstall.Controls.Add(Me.radEvilEris)
        Me.grbxROMToInstall.Controls.Add(Me.radxtrROM)
        Me.grbxROMToInstall.Controls.Add(Me.radVanillaDroid)
        Me.grbxROMToInstall.Controls.Add(Me.radKaosLegendary)
        Me.grbxROMToInstall.Controls.Add(Me.radErisOfficial)
        Me.grbxROMToInstall.Controls.Add(Me.radSenseable2)
        resources.ApplyResources(Me.grbxROMToInstall, "grbxROMToInstall")
        Me.grbxROMToInstall.Name = "grbxROMToInstall"
        Me.grbxROMToInstall.TabStop = False
        '
        'radCyanLightBoltFroyo
        '
        resources.ApplyResources(Me.radCyanLightBoltFroyo, "radCyanLightBoltFroyo")
        Me.radCyanLightBoltFroyo.Name = "radCyanLightBoltFroyo"
        Me.radCyanLightBoltFroyo.TabStop = True
        Me.radCyanLightBoltFroyo.UseVisualStyleBackColor = True
        '
        'radCyanLightBolt
        '
        resources.ApplyResources(Me.radCyanLightBolt, "radCyanLightBolt")
        Me.radCyanLightBolt.Name = "radCyanLightBolt"
        Me.radCyanLightBolt.TabStop = True
        Me.radCyanLightBolt.UseVisualStyleBackColor = True
        '
        'radIceRom
        '
        resources.ApplyResources(Me.radIceRom, "radIceRom")
        Me.radIceRom.Name = "radIceRom"
        Me.radIceRom.TabStop = True
        Me.radIceRom.UseVisualStyleBackColor = True
        '
        'radEvilEris
        '
        resources.ApplyResources(Me.radEvilEris, "radEvilEris")
        Me.radEvilEris.Name = "radEvilEris"
        Me.radEvilEris.TabStop = True
        Me.radEvilEris.UseVisualStyleBackColor = True
        '
        'radxtrROM
        '
        resources.ApplyResources(Me.radxtrROM, "radxtrROM")
        Me.radxtrROM.Name = "radxtrROM"
        Me.radxtrROM.TabStop = True
        Me.radxtrROM.UseVisualStyleBackColor = True
        '
        'radVanillaDroid
        '
        resources.ApplyResources(Me.radVanillaDroid, "radVanillaDroid")
        Me.radVanillaDroid.Name = "radVanillaDroid"
        Me.radVanillaDroid.TabStop = True
        Me.radVanillaDroid.UseVisualStyleBackColor = True
        '
        'radKaosLegendary
        '
        resources.ApplyResources(Me.radKaosLegendary, "radKaosLegendary")
        Me.radKaosLegendary.Name = "radKaosLegendary"
        Me.radKaosLegendary.TabStop = True
        Me.radKaosLegendary.UseVisualStyleBackColor = True
        '
        'radErisOfficial
        '
        resources.ApplyResources(Me.radErisOfficial, "radErisOfficial")
        Me.radErisOfficial.Name = "radErisOfficial"
        Me.radErisOfficial.TabStop = True
        Me.radErisOfficial.UseVisualStyleBackColor = True
        '
        'radSenseable2
        '
        resources.ApplyResources(Me.radSenseable2, "radSenseable2")
        Me.radSenseable2.Name = "radSenseable2"
        Me.radSenseable2.TabStop = True
        Me.radSenseable2.UseVisualStyleBackColor = True
        '
        'btnFlashSplashImage
        '
        resources.ApplyResources(Me.btnFlashSplashImage, "btnFlashSplashImage")
        Me.btnFlashSplashImage.Name = "btnFlashSplashImage"
        Me.btnFlashSplashImage.UseVisualStyleBackColor = True
        '
        'btnBackupAppSettings
        '
        resources.ApplyResources(Me.btnBackupAppSettings, "btnBackupAppSettings")
        Me.btnBackupAppSettings.Name = "btnBackupAppSettings"
        Me.btnBackupAppSettings.UseVisualStyleBackColor = True
        '
        'Button1
        '
        resources.ApplyResources(Me.Button1, "Button1")
        Me.Button1.Name = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'Button2
        '
        resources.ApplyResources(Me.Button2, "Button2")
        Me.Button2.Name = "Button2"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'MainForm
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.HTC_Eris_Master.My.Resources.Resources.android
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnBackupAppSettings)
        Me.Controls.Add(Me.btnFlashSplashImage)
        Me.Controls.Add(Me.grbxROMToInstall)
        Me.Controls.Add(Me.chbxRecoveryImage)
        Me.Controls.Add(Me.lblTotalDownloaded)
        Me.Controls.Add(Me.prgsTestProgressBar)
        Me.Controls.Add(Me.lblROOTRequired)
        Me.Controls.Add(Me.chbxROOTImage)
        Me.Controls.Add(Me.btnStartDownloads)
        Me.Controls.Add(Me.btnReinstallAllAppsFromSDCARD)
        Me.Controls.Add(Me.btnBackupAppsToSDCARD)
        Me.Controls.Add(Me.lblApplicationManagement)
        Me.Controls.Add(Me.lblHeading)
        Me.Controls.Add(Me.btnRootStep4)
        Me.Controls.Add(Me.btnRootStep3)
        Me.Controls.Add(Me.btnRootStep2)
        Me.Controls.Add(Me.lblRootingPhone)
        Me.Controls.Add(Me.btnReboot_Bootloader)
        Me.Controls.Add(Me.btnReboot_Recovery)
        Me.Controls.Add(Me.lblReboot_Options)
        Me.Controls.Add(Me.lblMainHeading)
        Me.Controls.Add(Me.btnReboot_Phone)
        Me.Name = "MainForm"
        Me.grbxROMToInstall.ResumeLayout(False)
        Me.grbxROMToInstall.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnReboot_Phone As System.Windows.Forms.Button
    Friend WithEvents lblMainHeading As System.Windows.Forms.Label
    Friend WithEvents lblReboot_Options As System.Windows.Forms.Label
    Friend WithEvents btnReboot_Recovery As System.Windows.Forms.Button
    Friend WithEvents btnReboot_Bootloader As System.Windows.Forms.Button
    Friend WithEvents lblRootingPhone As System.Windows.Forms.Label
    Friend WithEvents btnRootStep2 As System.Windows.Forms.Button
    Friend WithEvents btnRootStep3 As System.Windows.Forms.Button
    Friend WithEvents btnRootStep4 As System.Windows.Forms.Button
    Friend WithEvents OpenRomFile As System.Windows.Forms.OpenFileDialog
    Friend WithEvents lblHeading As System.Windows.Forms.Label
    Friend WithEvents lblApplicationManagement As System.Windows.Forms.Label
    Friend WithEvents btnBackupAppsToSDCARD As System.Windows.Forms.Button
    Friend WithEvents btnReinstallAllAppsFromSDCARD As System.Windows.Forms.Button
    Friend WithEvents btnStartDownloads As System.Windows.Forms.Button
    Friend WithEvents chbxROOTImage As System.Windows.Forms.CheckBox
    Friend WithEvents lblROOTRequired As System.Windows.Forms.Label
    Friend WithEvents prgsTestProgressBar As System.Windows.Forms.ProgressBar
    Friend WithEvents lblTotalDownloaded As System.Windows.Forms.Label
    Friend WithEvents chbxRecoveryImage As System.Windows.Forms.CheckBox
    Friend WithEvents grbxROMToInstall As System.Windows.Forms.GroupBox
    Friend WithEvents radErisOfficial As System.Windows.Forms.RadioButton
    Friend WithEvents radSenseable2 As System.Windows.Forms.RadioButton
    Friend WithEvents radKaosLegendary As System.Windows.Forms.RadioButton
    Friend WithEvents btnFlashSplashImage As System.Windows.Forms.Button
    Friend WithEvents radxtrROM As System.Windows.Forms.RadioButton
    Friend WithEvents radVanillaDroid As System.Windows.Forms.RadioButton
    Friend WithEvents radEvilEris As System.Windows.Forms.RadioButton
    Friend WithEvents radIceRom As System.Windows.Forms.RadioButton
    Friend WithEvents btnBackupAppSettings As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents radCyanLightBolt As System.Windows.Forms.RadioButton
    Friend WithEvents radCyanLightBoltFroyo As System.Windows.Forms.RadioButton
    Friend WithEvents Button2 As System.Windows.Forms.Button

End Class
