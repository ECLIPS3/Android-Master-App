'***************************************************
' Copyright (c) 2010 - 2011
' Original Devs:
' Mike Wohlrab (ECLIPS3)
' James Lichtenstiger (jamezelle)
'
' This program is now open source
' but we have a few requirements:
' 1) This must ALWAYS remaine free
' 2) You must keep all license and credits
' to the original devs in here. i.e. Credits Page
'
' You may contact us by email:
' Mike: Mike@mikewohlrab.com
' Or on IRC:
' irc.andirc.net #DroidEris #Zion
' 
' All Rights Reserved
'***************************************************

Option Strict On
Imports System.IO
Imports System.Net
Imports System.Net.Mail

Public Class MainForm

    '***********************************************************************************************************************************
    'Get os version to set os specific variables. - DONE
    '***********************************************************************************************************************************
#Region "Gather OS Information"

    Function GetOSVersion() As String
        Select Case Environment.OSVersion.Platform
            Case PlatformID.Win32S
                Return "Win 3.1"
            Case PlatformID.Win32Windows
                Select Case Environment.OSVersion.Platform
                    Case 0
                        Return "Win95"
                    Case CType(10, PlatformID)
                        Return "win98"
                    Case CType(90, PlatformID)
                        Return "WinME"
                    Case Else
                        Return "Unknown"
                End Select
            Case PlatformID.Win32NT
                Select Case Environment.OSVersion.Version.Major
                    Case 3
                        Return "NT 3.51"
                    Case 4
                        Return "NT 4.0"
                    Case 5
                        Select Case _
                            Environment.OSVersion.Version.Minor
                            Case 0
                                Return "Win2000"
                                'this is the one we are looking for in particular
                            Case 1
                                blnOSIsXP = True
                                Return "WinXP"
                                'dont need to go on from here
                            Case Else
                                Return "Unknown"
                        End Select
                    Case Else
                        Return "Unknown"
                End Select
            Case Else
                Return "Unknown"
        End Select
    End Function

#End Region

    '***********************************************************************************************************************************
    'Set Global Variables - DONE
    '***********************************************************************************************************************************
#Region "Global Variables"
    Declare Function SetProcessWorkingSetSize Lib "kernel32.dll" (ByVal process As IntPtr, ByVal minimumWorkingSetSize As Integer, ByVal maximumWorkingSetSize As Integer) As Integer

    Dim FolderBrowserDialog1 As New FolderBrowserDialog
    Dim strSDKLocation As String
    Dim strEMA_Location As String
    Dim strWhatRom As String
    Dim OpenFileDialog1 As New OpenFileDialog
    Dim strQuote As String = Chr(34)
    Dim myProcess As Process = New Process()
    Dim strCopyArguments As String
    Dim strCMDLocation As String
    Dim blnStep5WhatRomYesNo As Boolean
    Dim blnDeviceState As Boolean
    Dim fileImageToFlash As String
    Dim blnNoSplashImage As Boolean
    Dim strADB As String = "adb.exe"
    Dim strFastboot As String = "fastboot.exe"
    Dim strBegCMDProcess As String = "/C cd && "
    Dim strADBPush As String = " push "
    Dim strADBPull As String = " pull "
    Dim strSDCARD As String = " /sdcard"
    Dim strErisOfficial, strKaosLegendary, strSenseable, strxtrROM, strVanillaDroid, strEvilEris, strIceRom, strCyanLightBolt, strCyanLightBoltFroyo As String
    Dim strWhatRomInstall As String
    Dim blnRomIsDownloaded, blnROOTRomIsDownloaded, blnRecoveryIsDownloaded As Boolean
    Dim strROOTRomWasDownloaded, strRecoveryWasDownloaded As String
    Dim blnAllDownloadedOK As Boolean = False
    Dim blnOSIsXP As Boolean = False
    Dim strUrl As String
    Dim strRomName As String

    'variables for error logs
    Dim path, errorfile As String
    Dim nFileNum As Short
    Dim Filename As String
    Dim dTaskID As Double



#End Region

    '***********************************************************************************************************************************
    '  Checks for and sets Android SDK Tools Directory on form load
    '***********************************************************************************************************************************
#Region "Form Load Options"

    Private Sub MainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        strEMA_Location = FileSystem.CurDir
        strSDKLocation = FileSystem.CurDir & "\EMA_Files\"

        strErisOfficial = "_UPLOAD/_ROMS/erisofficial.zip"
        strKaosLegendary = "_UPLOAD/_ROMS/KaosLegendary012.zip"
        strSenseable = "_UPLOAD/_ROMS/senseable.zip"
        strxtrROM = "_UPLOAD/_ROMS/xtrROM.zip"
        strVanillaDroid = "_UPLOAD/_ROMS/vanilladroid.zip"
        strEvilEris = "_UPLOAD/_ROMS/evileris.zip"
        strIceRom = "_UPLOAD/_ROMS/icerom.zip"
        strCyanLightBolt = "_UPLOAD/_ROMS/CyanLightBolt.zip"
        strCyanLightBoltFroyo = "_UPLOAD/_ROMS/CyanLightBoltFroyo.zip"

        MessageBox.Show("WARNING, we will not be held responsible if anything bad happens to your phone by using this program. We will however do our best to solve any issue that you are having with it." & vbNewLine & vbNewLine & "Make sure USB Debugging is turned on or this application will not work. To do so go to:" & vbNewLine & vbNewLine & "Settings > Applications > Development > check USB Debugging" & vbNewLine & vbNewLine & "Make sure you keep your phone plugged in at ALL times untill you have finished.")

        lblHeading.Text = "This application has been created by ECLIPS3 and Jamezelle of XDA-DEVELOPERS.COM. Thanks go out to everyone on the IRC channel for helping us out with this."

        FormBorderStyle = FormBorderStyle.FixedSingle


    End Sub
#End Region

    '***********************************************************************************************************************************
    '  Reboot The Phone
    '***********************************************************************************************************************************
#Region "Reboot Phone"
    Private Sub Reboot_Phone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReboot_Phone.Click

        reboot_phone()

    End Sub

    Sub reboot_phone()
        Dim strReboot As String
        Device_State()

        If blnDeviceState = True Then

            New_Proccess()

            strReboot = " reboot"
            strCopyArguments = strBegCMDProcess & strQuote & strSDKLocation & strADB & strQuote & strReboot
            Start_New_CMD()

            MessageBox.Show("Phone is now rebooting")

        End If
    End Sub
#End Region

    '***********************************************************************************************************************************
    '  Reboot into Recovery Mode
    '***********************************************************************************************************************************
#Region "Reboot into Recovery"
    Private Sub Reboot_Recovery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReboot_Recovery.Click

        Reboot_Recovery()

    End Sub

    Sub Reboot_Recovery()

        Dim strRebootRecovery As String
        Device_State()

        If blnDeviceState = True Then

            New_Proccess()

            strRebootRecovery = " reboot recovery"
            strCopyArguments = strBegCMDProcess & strQuote & strSDKLocation & strADB & strQuote & strRebootRecovery
            Start_New_CMD()

            MessageBox.Show("Phone is now rebooting into recovery mode")

        End If

    End Sub
#End Region

    '***********************************************************************************************************************************
    '  Reboot into Bootloader Mode
    '***********************************************************************************************************************************
#Region "Reboot into Bootloader"
    Private Sub Reboot_Bootloader_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReboot_Bootloader.Click

        reboot_bootloader()

    End Sub

    Sub reboot_bootloader()

        Dim strRebootBootloader As String
        Device_State()

        If blnDeviceState = True Then

            New_Proccess()

            strRebootBootloader = " reboot bootloader"
            strCopyArguments = strBegCMDProcess & strQuote & strSDKLocation & strADB & strQuote & strRebootBootloader
            Start_New_CMD()

            MessageBox.Show("Phone is now rebooting into bootloader mode")

        End If

    End Sub
#End Region

    '***********************************************************************************************************************************
    ' Flushes the Memory being used by windows to limit the apps memory uses. helpful after downloads subs.
    '***********************************************************************************************************************************
    Public Sub FlushMemory()
        Try
            GC.Collect()
            GC.WaitForPendingFinalizers()
            If (Environment.OSVersion.Platform = PlatformID.Win32NT) Then
                SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1)
                'I dont know if ("ApplicationName") needs to be changed. seems to work without changing it
                Dim myProcesses As Process() = Process.GetProcessesByName("ApplicationName")
                Dim myProcess As Process
                'Dim ProcessInfo As Process
                For Each myProcess In myProcesses
                    SetProcessWorkingSetSize(myProcess.Handle, -1, -1)
                Next myProcess
            End If
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
    End Sub

    '***********************************************************************************************************************************
    '  Root Step 1 - Download the ROMs and recovery files to the PC
    '***********************************************************************************************************************************
#Region "Root Step 1 - Download files to PC"

    Private Sub btnStartDownloads_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStartDownloads.Click

        MessageBox.Show("All selected files will now be downloaded. When ALL the files are DONE downloading, you will be notified that they are then done. Until then, please be patient and wait for them to download.")

        Try
            Dim strDownloadFileLink As String = ""
            Dim strDownloadFileLinkMirror As String = ""
            Dim strLinkToFileDownload(20) As String
            Dim strLinkToFileDownloadMirror(20) As String

            'Senseable 3.1 - Option 0
            strLinkToFileDownload(0) = "http://www.ctswebdesign.com/up/uploads/jamezelle/ad6491_sense-able31.zip"
            strLinkToFileDownloadMirror(0) = "http://android.grdlock.net/index.php?action=downloadfile&filename=Sense-able%203.1.zip&directory=Jamezelles%20Roms&"

            'Evil Eris 3.0 - Option 1
            strLinkToFileDownload(1) = "http://www.ctswebdesign.com/up/uploads/Framework/adf746_evileris30.zip"
            strLinkToFileDownloadMirror(1) = "http://android.grdlock.net//index.php?action=downloadfile&filename=EvilEris3.0.zip&directory=Frameworks%20Roms&"

            'Eris Official 1.0 - Option 2
            strLinkToFileDownload(2) = "http://www.ctswebdesign.com/up/uploads/ivanmmj/ad5f4a_eris_official_10_oc.zip"
            strLinkToFileDownloadMirror(2) = "http://android.grdlock.net/index.php?action=downloadfile&filename=Eris_Official_0.8.1T2.zip&directory=Ivans%20Roms&"

            'xtrROM 3.0.3 - Option 3
            strLinkToFileDownload(3) = "http://zach.xtr.i6ix.com/xtrROM3.0.3-fixed.zip"
            strLinkToFileDownloadMirror(3) = "http://www.ctswebdesign.com/up/uploads/ECLIPS3/xtrrom303-fixed.zip"

            'Kaos Legendary v17 - Option 4
            strLinkToFileDownload(4) = ""
            strLinkToFileDownloadMirror(4) = ""

            'PB00IMG.zip root rom - 5
            strLinkToFileDownload(5) = "http://www.ctswebdesign.com/up/uploads/ECLIPS3/ad3468_pb00img-root.zip"
            strLinkToFileDownloadMirror(5) = "http://android.grdlock.net/index.php?action=downloadfile&filename=PB00IMG-root.zip&directory=PB00IMG%20Roms&"

            'recovery image - 6
            strLinkToFileDownload(6) = "http://www.ctswebdesign.com/up/uploads/ECLIPS3/ade177_recovery-ra-eris-v162.img"
            strLinkToFileDownloadMirror(6) = "http://android.grdlock.net/index.php?action=downloadfile&filename=recovery-RA-eris-v1.6.2.img&directory=Extras&"

            'vanilla droid - 7
            strLinkToFileDownload(7) = ""
            strLinkToFileDownloadMirror(7) = ""

            'Ice Rom 2.3 MMS Fix - 8
            strLinkToFileDownload(8) = ""
            strLinkToFileDownloadMirror(8) = ""

            'Cyanogen Eris Lighting Bolt 2.8 - 9
            strLinkToFileDownload(9) = "http://www.ctswebdesign.com/up/uploads/Conap/adff51_cyanogenelb28.zip"
            strLinkToFileDownloadMirror(9) = "http://droid-roms.net/roms/CyanogenELB2.8.zip"

            'Cyanogen Eris Lighting Bolt Froyo 1.4 - 10 
            strLinkToFileDownload(10) = "http://www.ctswebdesign.com/up/uploads/Conap/celbfroyo14.zip"
            strLinkToFileDownloadMirror(10) = "http://android.grdlock.net//index.php?action=downloadfile&filename=CELBFroyo1.4.zip&directory=Conaps%20Roms&"



            'Eris Official
            If radErisOfficial.Checked = True Then
                strWhatRomInstall = "ErisOfficial"
                strDownloadFileLink = strLinkToFileDownload(2)
                DownloadFiles(strDownloadFileLink, strDownloadFileLinkMirror)
                prgsTestProgressBar.Value = 0
                radErisOfficial.Checked = False
                blnRomIsDownloaded = True

                'Kaos Legendary
            ElseIf radKaosLegendary.Checked = True Then
                strWhatRomInstall = "Kaos"
                strDownloadFileLink = strLinkToFileDownload(4)
                DownloadFiles(strDownloadFileLink, strDownloadFileLinkMirror)
                prgsTestProgressBar.Value = 0
                radErisOfficial.Checked = False
                blnRomIsDownloaded = True

                'Senseable
            ElseIf radSenseable2.Checked = True Then
                strWhatRomInstall = "Senseable"
                strDownloadFileLink = strLinkToFileDownload(0)
                DownloadFiles(strDownloadFileLink, strDownloadFileLinkMirror)
                prgsTestProgressBar.Value = 0
                radErisOfficial.Checked = False
                blnRomIsDownloaded = True

                'xtrROM
            ElseIf radxtrROM.Checked = True Then
                strWhatRomInstall = "xtrROM"
                strDownloadFileLink = strLinkToFileDownload(3)
                DownloadFiles(strDownloadFileLink, strDownloadFileLinkMirror)
                prgsTestProgressBar.Value = 0
                radErisOfficial.Checked = False
                blnRomIsDownloaded = True

                'VanillaDroid
            ElseIf radVanillaDroid.Checked = True Then
                strWhatRomInstall = "VanillaDroid"
                strDownloadFileLink = strLinkToFileDownload(7)
                DownloadFiles(strDownloadFileLink, strDownloadFileLinkMirror)
                prgsTestProgressBar.Value = 0
                radErisOfficial.Checked = False
                blnRomIsDownloaded = True

                'Evil Eris
            ElseIf radEvilEris.Checked = True Then
                strWhatRomInstall = "EvilEris"
                strDownloadFileLink = strLinkToFileDownload(1)
                DownloadFiles(strDownloadFileLink, strDownloadFileLinkMirror)
                prgsTestProgressBar.Value = 0
                radErisOfficial.Checked = False
                blnRomIsDownloaded = True

                'Ice Rom
            ElseIf radIceRom.Checked = True Then
                strWhatRomInstall = "IceRom"
                strDownloadFileLink = strLinkToFileDownload(8)
                DownloadFiles(strDownloadFileLink, strDownloadFileLinkMirror)
                prgsTestProgressBar.Value = 0
                radErisOfficial.Checked = False
                blnRomIsDownloaded = True

                'Cyanogen Eris Lightning Bolt Rom
            ElseIf radCyanLightBolt.Checked = True Then
                strWhatRomInstall = "CyanLightBolt"
                strDownloadFileLink = strLinkToFileDownload(9)
                DownloadFiles(strDownloadFileLink, strDownloadFileLinkMirror)
                prgsTestProgressBar.Value = 0
                radErisOfficial.Checked = False
                blnRomIsDownloaded = True

                'Cyanogen Eris Lightning Bolt Froyo Rom
            ElseIf radCyanLightBoltFroyo.Checked = True Then
                strWhatRomInstall = "CyanLightBoltFroyo"
                strDownloadFileLink = strLinkToFileDownload(10)
                strDownloadFileLinkMirror = strLinkToFileDownloadMirror(10)
                DownloadFiles(strDownloadFileLink, strDownloadFileLinkMirror)
                prgsTestProgressBar.Value = 0
                radErisOfficial.Checked = False
                blnRomIsDownloaded = True

            Else

                grbxROMToInstall.Visible = True
                prgsTestProgressBar.Value = 0
                lblROOTRequired.Visible = True
                lblTotalDownloaded.Visible = True

            End If

            'Root Rom Download
            If chbxROOTImage.Checked = True Then
                downloadrootimage()
                chbxROOTImage.Visible = False
                chbxROOTImage.Checked = False
                blnROOTRomIsDownloaded = True
                strROOTRomWasDownloaded = "Root Rom Was Downloaded"
            Else
                strROOTRomWasDownloaded = "Root Rom Was NOT Downloaded"
            End If

            'Recovery Image Download
            If chbxRecoveryImage.Checked = True Then
                downloadrecoveryimage()
                ' chbxRecoveryImage.Visible = False
                chbxRecoveryImage.Checked = False
                blnRecoveryIsDownloaded = True
                strRecoveryWasDownloaded = "Recovery Image Was Downloaded"
            Else
                strRecoveryWasDownloaded = "Recovery Image Was NOT Downloaded"
            End If

            'Warns user if not all files are checked. Can still procede without checking all files though
            If (blnRecoveryIsDownloaded And blnROOTRomIsDownloaded And blnRomIsDownloaded) = False Then
                MessageBox.Show("A Custom ROM Files must be check, and ROOT ROM must be checked, and Recovery Image must be checked to get root. Please select those, then try downloading again.")
                blnAllDownloadedOK = True
            End If

            'Double check for those that did complete all downloads. Tells them to move on.
            If blnAllDownloadedOK = False Then
                MessageBox.Show("All files have now FINISHED downloading. Please move on to Step 2 - Get Phone Rooted.")
                grbxROMToInstall.Visible = False
                chbxRecoveryImage.Visible = False
                chbxROOTImage.Visible = False
                lblROOTRequired.Visible = False
            Else
                chbxRecoveryImage.Visible = True
                chbxROOTImage.Visible = True
                grbxROMToInstall.Visible = True
            End If

        Catch ex As Exception

            prgsTestProgressBar.Value = 0
            lblROOTRequired.Visible = True
            lblTotalDownloaded.Visible = True

            Dim strOSname As String = My.Computer.Info.OSFullName
            Dim strPathToApp As String = strEMA_Location & "\" & My.Application.Info.AssemblyName & ".exe"
            Dim strDateTime As String = CStr(DateTime.Now)
            Dim strUserName As String = My.User.Name

            'Code to make an error log
            Filename = "c:\AMA_Error_Log.txt"
            nFileNum = CShort(FreeFile())
            FileClose(nFileNum)
            FileOpen(nFileNum, Filename, OpenMode.Append)
            PrintLine(nFileNum, vbNewLine)
            PrintLine(nFileNum, "*********************************************************************************")
            PrintLine(nFileNum, vbNewLine)
            PrintLine(nFileNum, "Bug Report From: ", strDateTime)
            PrintLine(nFileNum, "Operating System: ", strOSname)
            PrintLine(nFileNum, "Path To App: ", strPathToApp)
            PrintLine(nFileNum, "PC/User Name: ", strUserName)
            PrintLine(nFileNum, "What Rom D/l: ", strWhatRomInstall)
            PrintLine(nFileNum, vbNewLine)
            PrintLine(nFileNum, "Error Log Report:")
            PrintLine(nFileNum, vbNewLine)
            PrintLine(nFileNum, ex.ToString)
            PrintLine(nFileNum, vbNewLine)
            FileClose(nFileNum)

            'opens file in notepad for user to read this is really only for testing bc the user
            'wont need to see this, we can ask them to open the file if we need whats in it incase the email didnt send
            'path = "C:\WINDOWS\notepad.exe"
            'errorfile = Filename
            'dTaskID = Shell(path & " " & errorfile, AppWinStyle.NormalFocus)

            SendEmail()

            MessageBox.Show("Unable to download the files. The most likely cause is that this app cannot find the requested file, or you are using Windows XP." & vbNewLine & vbNewLine & "A log file has been sent to us so we can work on fixing the issue.")

        End Try

        FlushMemory()

    End Sub

    'code to combine the d/l code into one sub w/ variables
    Sub DownloadFiles(ByVal strDownloadFileLink As String, ByVal strDownloadFileLinkMirror As String)

        'First tries to download from first link. If fails in this Try, goes to Catch, and Downloads from Mirror, on Second try, if failed again, sends email of log.
        Dim wr As HttpWebRequest = CType(WebRequest.Create(strDownloadFileLink), HttpWebRequest)
        Dim ws As HttpWebResponse = CType(wr.GetResponse(), HttpWebResponse)
        Dim str As Stream = ws.GetResponseStream()
        Dim intFileSize As Integer
        intFileSize = CInt(ws.ContentLength)
        Dim inBuf(intFileSize) As Byte
        Dim bytesToRead As Integer = intFileSize
        Dim bytesRead As Integer = 0
        Dim dblProgPercent As Double

        prgsTestProgressBar.Minimum = 0
        prgsTestProgressBar.Maximum = intFileSize
        prgsTestProgressBar.Value = 0

        While bytesToRead > 0

            Dim n As Integer = str.Read(inBuf, bytesRead, bytesToRead)
            If n = 0 Then
                Exit While
            End If

            'delay which is needed for xp os support. if the file downloads too fast, then it will say
            'that the file cannot be downloaded / found and to try a different file
            'EDIT By JAMES -  0.5 seems to be the sweet spot
            GetOSVersion()
            If blnOSIsXP = True Then
                Delay(0.9)
            End If
            'END EDIT By JAMES

            bytesRead += n
            bytesToRead -= n

            ' Allow windows messages to be processed
            Application.DoEvents()

            prgsTestProgressBar.Value += n
            dblProgPercent = (prgsTestProgressBar.Value / prgsTestProgressBar.Maximum) * 100
            dblProgPercent = Math.Round(dblProgPercent, 2)

            lblTotalDownloaded.Text = dblProgPercent & "% Total Done"
            lblTotalDownloaded.Refresh()

        End While
        Dim fstr As New FileStream("./EMA_Files/_UPLOAD/_ROMS/" & strWhatRomInstall.ToLower & ".zip", FileMode.OpenOrCreate, FileAccess.Write)
        fstr.Write(inBuf, 0, bytesRead)
        str.Close()
        fstr.Close()

        'checks file size of rom downloaded against pre populated value to make sure we got the whole file. md5 coming later
        Dim MyFile As New FileInfo(strEMA_Location & "/EMA_Files/_UPLOAD/_ROMS/" & strWhatRomInstall.ToLower & ".zip")
        Dim FileSize As Long = MyFile.Length

        Dim intSizeOfCELBFroyo As Integer = 51301452

        'if size not equal, try d/l again
        If FileSize <> intSizeOfCELBFroyo Then

            Dim wr2 As HttpWebRequest = CType(WebRequest.Create(strDownloadFileLink), HttpWebRequest)
            Dim ws2 As HttpWebResponse = CType(wr2.GetResponse(), HttpWebResponse)
            Dim str2 As Stream = ws2.GetResponseStream()
            Dim intFileSize2 As Integer
            intFileSize2 = CInt(ws2.ContentLength)
            Dim inBuf2(intFileSize2) As Byte
            Dim bytesToRead2 As Integer = intFileSize2
            Dim bytesRead2 As Integer = 0
            Dim dblProgPercent2 As Double

            prgsTestProgressBar.Minimum = 0
            prgsTestProgressBar.Maximum = intFileSize2
            prgsTestProgressBar.Value = 0

            While bytesToRead2 > 0

                Dim n As Integer = str2.Read(inBuf2, bytesRead2, bytesToRead2)
                If n = 0 Then
                    Exit While
                End If

                'delay which is needed for xp os support. if the file downloads too fast, then it will say
                'that the file cannot be downloaded / found and to try a different file
                'EDIT By JAMES -  0.5 seems to be the sweet spot
                GetOSVersion()
                If blnOSIsXP = True Then
                    Delay(0.9)
                End If
                'END EDIT By JAMES

                bytesRead2 += n
                bytesToRead2 -= n

                ' Allow windows messages to be processed
                Application.DoEvents()

                prgsTestProgressBar.Value += n
                dblProgPercent2 = (prgsTestProgressBar.Value / prgsTestProgressBar.Maximum) * 100
                dblProgPercent2 = Math.Round(dblProgPercent2, 2)

                lblTotalDownloaded.Text = dblProgPercent2 & "% Total Done"
                lblTotalDownloaded.Refresh()

            End While
            Dim fstr2 As New FileStream("./EMA_Files/_UPLOAD/_ROMS/" & strWhatRomInstall.ToLower & ".zip", FileMode.OpenOrCreate, FileAccess.Write)
            fstr2.Write(inBuf2, 0, bytesRead2)
            str2.Close()
            fstr2.Close()

        End If



    End Sub

    'Download for ROOT Image
    Sub downloadrootimage()

        Dim strLinkToFile As String = "http://android.grdlock.net/index.php?action=downloadfile&filename=PB00IMG-root.zip&directory=PB00IMG%20Roms&"

        'Dim strMirror As String = ""
        Dim wr As HttpWebRequest = CType(WebRequest.Create(strLinkToFile), HttpWebRequest)
        Dim ws As HttpWebResponse = CType(wr.GetResponse(), HttpWebResponse)
        Dim str As Stream = ws.GetResponseStream()
        Dim intFileSize As Integer
        intFileSize = CInt(ws.ContentLength)
        Dim inBuf(intFileSize) As Byte
        Dim bytesToRead As Integer = intFileSize
        Dim bytesRead As Integer = 0
        Dim dblProgPercent As Double

        prgsTestProgressBar.Minimum = 0
        prgsTestProgressBar.Maximum = intFileSize
        prgsTestProgressBar.Value = 0

        While bytesToRead > 0

            Dim n As Integer = str.Read(inBuf, bytesRead, bytesToRead)
            If n = 0 Then
                Exit While
            End If

            'delay which is needed for xp os support. if the file downloads too fast, then it will say
            'that the file cannot be downloaded / found and to try a different file
            'EDIT By JAMES -  0.5 seems to be the sweet spot
            GetOSVersion()
            If blnOSIsXP = True Then
                Delay(0.7)
            End If
            bytesRead += n
            bytesToRead -= n
            'END EDIT By JAMES
            Application.DoEvents() ' Allow windows messages to be processed

            prgsTestProgressBar.Value += n

            dblProgPercent = (prgsTestProgressBar.Value / prgsTestProgressBar.Maximum) * 100

            dblProgPercent = Math.Round(dblProgPercent, 2)

            lblTotalDownloaded.Text = dblProgPercent & "% Total Done"
            lblTotalDownloaded.Refresh()

        End While
        Dim fstr As New FileStream("./EMA_Files/_UPLOAD/ROOT/PB00IMG.zip", FileMode.OpenOrCreate, FileAccess.Write)
        fstr.Write(inBuf, 0, bytesRead)
        str.Close()
        fstr.Close()

    End Sub

    'Download for Recovery Image
    Sub downloadrecoveryimage()

        Dim strLinkToFile As String = "http://android.grdlock.net/index.php?action=downloadfile&filename=recovery-RA-eris-v1.6.2.img&directory=Extras&"

        'Dim strMirror As String = ""
        Dim wr As HttpWebRequest = CType(WebRequest.Create(strLinkToFile), HttpWebRequest)
        Dim ws As HttpWebResponse = CType(wr.GetResponse(), HttpWebResponse)
        Dim str As Stream = ws.GetResponseStream()
        Dim intFileSize As Integer
        intFileSize = CInt(ws.ContentLength)
        Dim inBuf(intFileSize) As Byte
        Dim bytesToRead As Integer = intFileSize
        Dim bytesRead As Integer = 0
        Dim dblProgPercent As Double

        prgsTestProgressBar.Minimum = 0
        prgsTestProgressBar.Maximum = intFileSize
        prgsTestProgressBar.Value = 0

        While bytesToRead > 0

            Dim n As Integer = str.Read(inBuf, bytesRead, bytesToRead)
            If n = 0 Then
                Exit While
            End If

            'delay which is needed for xp os support. if the file downloads too fast, then it will say
            'that the file cannot be downloaded / found and to try a different file
            'EDIT By JAMES -  0.5 seems to be the sweet spot
            GetOSVersion()
            If blnOSIsXP = True Then
                Delay(0.7)
            End If
            bytesRead += n
            bytesToRead -= n
            'END EDIT By JAMES
            Application.DoEvents() ' Allow windows messages to be processed

            prgsTestProgressBar.Value += n

            dblProgPercent = (prgsTestProgressBar.Value / prgsTestProgressBar.Maximum) * 100

            dblProgPercent = Math.Round(dblProgPercent, 2)

            lblTotalDownloaded.Text = dblProgPercent & "% Total Done"
            lblTotalDownloaded.Refresh()

        End While
        Dim fstr As New FileStream("./EMA_Files/_UPLOAD/ROOT/recovery.img", FileMode.OpenOrCreate, FileAccess.Write)
        fstr.Write(inBuf, 0, bytesRead)
        str.Close()
        fstr.Close()

    End Sub


#End Region

    '***********************************************************************************************************************************
    '  Root Step 2 - Getting Root 
    '***********************************************************************************************************************************
#Region "Root Step 2 - Getting Root"
    Private Sub btnRootStep_2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRootStep2.Click

        Dim strRootROM As String = "/EMA_Files/_UPLOAD/ROOT/PB00IMG.zip"

        Device_State()

        If blnDeviceState = True Then

            MessageBox.Show("Files will be copied to your SDCARD. This process will take a little while. Please be patient. A message will appear with instructions on when to move on.")
            Application.DoEvents()
            If File.Exists(strEMA_Location & strRootROM) = True Then
                New_Proccess()
                strCopyArguments = strBegCMDProcess & strQuote & strSDKLocation & strADB & strQuote & strADBPush & strQuote & strEMA_Location & strRootROM & strQuote & strSDCARD
                Application.DoEvents()
                Start_New_CMD()

                MessageBox.Show("YOU CAN KEEP THIS MESSAGE BOX OPEN WILE DOING THESE NEXT STEPS." & vbNewLine & vbNewLine & "Phone now needs to be rebooted into bootloader mode. To do that power down the phone. Then hold down the VOLUME DOWN button while holding the POWER END button." & vbNewLine & vbNewLine & "When the phone turns on it will start checking for a bunch of files. It will ask you if you want to start the update." & vbNewLine & vbNewLine & "Hit VOLUME UP button for YES, or you may have to push in on the TRACKBALL for YES. The phone will then restart and check the files again. Let it do its thing." & vbNewLine & vbNewLine & "DO NOT TOUCH ANY BUTTONS WHILE IT IS UPDATING THE PHONE OR YOU COULD BRICK IT." & vbNewLine & vbNewLine & "When it finishes writing the update it will ask if you want to reboot the device." & vbNewLine & vbNewLine & "PUSH THE TRACKBALL in for YES. Now just wait about 5 minutes as it will take a really long time to startup the phone." & vbNewLine & vbNewLine & "When the phone asks you to do the setup, YOU NOW HAVE ROOT. CONGRATULATIONS. DO NOT DO THE SETUP and continue straight to Step 3 - Recovery Image.")

                grbxROMToInstall.Visible = False
                chbxRecoveryImage.Visible = False
                chbxROOTImage.Visible = False
                lblROOTRequired.Visible = False

            Else

                MessageBox.Show("Cannot find the Root ROM needed to install Root your phone. Please download the MANDATORY R00T Files.")

                grbxROMToInstall.Visible = False
                chbxRecoveryImage.Visible = True
                chbxROOTImage.Visible = True
                lblROOTRequired.Visible = True

            End If

        End If

    End Sub
#End Region

    '***********************************************************************************************************************************
    '  Root Step 3 - Recovery Image
    '***********************************************************************************************************************************
#Region "Root Step 3 - Recovery Image"
    Private Sub btnRootStep3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRootStep3.Click

        Dim strRecImage As String = "/EMA_Files/_UPLOAD/ROOT/recovery.img"
        Dim strFlashRecovery As String = " flash recovery "

        Device_State()

        If blnDeviceState = True Then

            If File.Exists(strEMA_Location & strRecImage) = True Then

                reboot_bootloader()

                'possibly use .sleep() instead of delay function
                Delay(12)

                New_Proccess()
                strCopyArguments = strBegCMDProcess & strQuote & strSDKLocation & strFastboot & strQuote & strFlashRecovery & strQuote & strEMA_Location & strRecImage & strQuote
                Start_New_CMD()

                Delay(2)

                New_Proccess()
                strCopyArguments = strBegCMDProcess & strQuote & strSDKLocation & strFastboot & strQuote & " reboot"
                Start_New_CMD()


                MessageBox.Show("Phone now has a custom recovery image and has been rebooted. After phone has been fully rebooted, continue to Step 4 - New ROM Install.")

                'Hide options not needed for the user
                grbxROMToInstall.Visible = False
                grbxROMToInstall.Text = "                                                       Select 1 (ONE) ROM to UPLOAD to your Phone."
                chbxRecoveryImage.Visible = False
                chbxROOTImage.Visible = False
                lblROOTRequired.Visible = False

            Else
                MessageBox.Show("Cannot find the files needed to install recovery mode on your phone. Please download the MANDATORY R00T Files.")

                'Show options user needs to fix
                grbxROMToInstall.Visible = False
                chbxRecoveryImage.Visible = True
                chbxROOTImage.Visible = True
                lblROOTRequired.Visible = True

            End If
        End If
    End Sub
#End Region

    '***********************************************************************************************************************************
    '  Root Step 4 - Custom ROM Install
    '***********************************************************************************************************************************
#Region "Root Step 4 - Custom ROM Install"
    Private Sub btnRootStep4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRootStep4.Click

        Dim strRomToUse As String

        Device_State()

        If blnDeviceState = True Then

            If File.Exists(strSDKLocation & strErisOfficial) = True Or File.Exists(strSDKLocation & strKaosLegendary) = True Or File.Exists(strSDKLocation & strSenseable) = True Or File.Exists(strSDKLocation & strxtrROM) = True Or File.Exists(strSDKLocation & strVanillaDroid) = True Or File.Exists(strSDKLocation & strEvilEris) = True Or File.Exists(strSDKLocation & strIceRom) = True Or File.Exists(strSDKLocation & strCyanLightBolt) = True Or File.Exists(strSDKLocation & strCyanLightBoltFroyo) = True Then
                Try
                    MessageBox.Show("The ROM will now be copied to your SDCARD. It will take a while, so please be patient.")
                    New_Proccess()

                    'Eris Official
                    If strWhatRomInstall = "ErisOfficial" Then
                        strRomToUse = strSDKLocation & strErisOfficial
                        strCopyArguments = strBegCMDProcess & strQuote & strSDKLocation & strADB & strQuote & strADBPush & strQuote & strRomToUse & strQuote & strSDCARD

                        'Kaos Legendary
                    ElseIf strWhatRomInstall = "Kaos" Then
                        strRomToUse = strSDKLocation & strKaosLegendary
                        strCopyArguments = strBegCMDProcess & strQuote & strSDKLocation & strADB & strQuote & strADBPush & strQuote & strRomToUse & strQuote & strSDCARD

                        'Senseable
                    ElseIf strWhatRomInstall = "Senseable" Then
                        strRomToUse = strSDKLocation & strSenseable
                        strCopyArguments = strBegCMDProcess & strQuote & strSDKLocation & strADB & strQuote & strADBPush & strQuote & strRomToUse & strQuote & strSDCARD

                        'xtrROM
                    ElseIf strWhatRomInstall = "xtrROM" Then
                        strRomToUse = strSDKLocation & strxtrROM
                        strCopyArguments = strBegCMDProcess & strQuote & strSDKLocation & strADB & strQuote & strADBPush & strQuote & strRomToUse & strQuote & strSDCARD

                        'VanillaDroid
                    ElseIf strWhatRomInstall = "VanillaDroid" Then
                        strRomToUse = strSDKLocation & strVanillaDroid
                        strCopyArguments = strBegCMDProcess & strQuote & strSDKLocation & strADB & strQuote & strADBPush & strQuote & strRomToUse & strQuote & strSDCARD

                        'Evil Eris
                    ElseIf strWhatRomInstall = "EvilEris" Then
                        strRomToUse = strSDKLocation & strEvilEris
                        strCopyArguments = strBegCMDProcess & strQuote & strSDKLocation & strADB & strQuote & strADBPush & strQuote & strRomToUse & strQuote & strSDCARD

                        'Ice Rom
                    ElseIf strWhatRomInstall = "IceRom" Then
                        strRomToUse = strSDKLocation & strIceRom
                        strCopyArguments = strBegCMDProcess & strQuote & strSDKLocation & strADB & strQuote & strADBPush & strQuote & strRomToUse & strQuote & strSDCARD

                        'Cyanogen Eris Lightning Bolt
                    ElseIf strWhatRomInstall = "CyanLightBolt" Then
                        strRomToUse = strSDKLocation & strCyanLightBolt
                        strCopyArguments = strBegCMDProcess & strQuote & strSDKLocation & strADB & strQuote & strADBPush & strQuote & strRomToUse & strQuote & strSDCARD

                        'Cyanogen Eris Lightning Bolt Froyo
                    ElseIf strWhatRomInstall = "CyanLightBoltFroyo" Then
                        strRomToUse = strSDKLocation & strCyanLightBoltFroyo
                        strCopyArguments = strBegCMDProcess & strQuote & strSDKLocation & strADB & strQuote & strADBPush & strQuote & strRomToUse & strQuote & strSDCARD

                    End If

                    Start_New_CMD()
                    Del_Root_Rom()
                    Reboot_Recovery()

                    MessageBox.Show("YOU MAY KEEP THIS OPEN WHILE YOU DO THE NEXT STEPS. DO NOT CLOSE OUT." & vbNewLine & vbNewLine & "Phone has now been rebooted with a custom ROM copied to sdcard." & vbNewLine & vbNewLine & "At recovery menu scroll down to WIPE and push in on trackball to select options." & vbNewLine & "Scroll down to WIPE DATA / FACTORY RESET and push trackball to confirm." & vbNewLine & "Next scroll down to WIPE DALVIK - CACHE and push trackball to confirm." & vbNewLine & "HIT VOLUME DOWN button to return to previous menu." & vbNewLine & "SCROLL to FLASH ZIP FROM SDCARD and select the name of the ROM you want to use and PUSH trackball to select ROM, then again to confirm." & vbNewLine & vbNewLine & "Wait for process to finish which will take a while." & vbNewLine & vbNewLine & "Finally PUSH trackball to reboot system now and then wait again for really long bootup process. CONGRATS, you are now R00TED. You may now start using your phone now and setting things up.")

                Catch ex As Exception

                    Dim strOSname As String = My.Computer.Info.OSFullName
                    Dim strPathToApp As String = strEMA_Location & "\" & My.Application.Info.AssemblyName & ".exe"
                    Dim strDateTime As String = CStr(DateTime.Now)
                    Dim strUserName As String = My.User.Name

                    'Code to make an error log
                    Filename = "c:\AMA_Error_Log.txt"
                    nFileNum = CShort(FreeFile())
                    FileClose(nFileNum)
                    FileOpen(nFileNum, Filename, OpenMode.Append)
                    PrintLine(nFileNum, vbNewLine)
                    PrintLine(nFileNum, "*********************************************************************************")
                    PrintLine(nFileNum, vbNewLine)
                    PrintLine(nFileNum, "Bug Report From: ", strDateTime)
                    PrintLine(nFileNum, "Operating System: ", strOSname)
                    PrintLine(nFileNum, "Path To App: ", strPathToApp)
                    PrintLine(nFileNum, "PC/User Name: ", strUserName)
                    PrintLine(nFileNum, "Rom To Install: ", strWhatRomInstall)
                    PrintLine(nFileNum, vbNewLine)
                    PrintLine(nFileNum, "Error Log Report:")
                    PrintLine(nFileNum, vbNewLine)
                    PrintLine(nFileNum, ex.ToString)
                    PrintLine(nFileNum, vbNewLine)
                    FileClose(nFileNum)

                    SendEmail()

                    MessageBox.Show("Cannot load the needed files to your phone. Please download a ROM using the File Downloader.")

                End Try
            Else
                MessageBox.Show("Cannot find the files needed to Upload Custom ROM to your phone. Please download a ROM using the File Downloader.")
            End If
        Else
            MessageBox.Show("Unable to get device state.")
        End If

    End Sub
#End Region

    '***********************************************************************************************************************************
    '  Delete Original ROM off of phone. No further use for it - DONE
    '***********************************************************************************************************************************
#Region "Delete Original Root ROM"
    Sub Del_Root_Rom()

        New_Proccess()
        Dim strDelFile As String

        strDelFile = " shell rm sdcard/PB00IMG.zip"
        strCopyArguments = strBegCMDProcess & strQuote & strSDKLocation & strADB & strQuote & strDelFile
        Start_New_CMD()

    End Sub
#End Region

    '***********************************************************************************************************************************
    '  Choose what ROM to upload to the phone
    '***********************************************************************************************************************************
#Region "Choose ROM to upload"
    Sub what_rom_to_use()

        Dim str1, str2 As String

        With OpenFileDialog1
            .CheckFileExists = True
            .ShowReadOnly = True

            ' Select the C:\Windows directory on entry.
            str1 = "/EMA_Files/_UPLOAD/_ROMS"
            str2 = strQuote & strEMA_Location & str1 & strQuote
            .InitialDirectory = str2

            ' Prompt the user with a custom message.
            .Title = "Select the ROM you would like to upload to your phone."

            If .ShowDialog = DialogResult.OK Then
                strWhatRom = .FileName
                blnStep5WhatRomYesNo = True
            Else
                blnStep5WhatRomYesNo = False
            End If
        End With

    End Sub
#End Region

    '***********************************************************************************************************************************
    '  ADB Permissions 
    '***********************************************************************************************************************************
#Region "ADB Shell SU Permissions"
    Sub ADB_Shell_SU()

        New_Proccess()
        Dim str3 As String

        str3 = " shell su"
        strCopyArguments = strBegCMDProcess & strQuote & strSDKLocation & strADB & strQuote & str3

        Start_New_CMD()

    End Sub
#End Region

    '***********************************************************************************************************************************
    '  New CMD Processs - DONE
    '***********************************************************************************************************************************
#Region "Start new CMD Process"

    Sub Start_New_CMD()

        'What proccess to start and what commands to start with
        myProcess.StartInfo.FileName = "cmd.exe"
        myProcess.StartInfo.Arguments = strCopyArguments

        'Start the process in a hidden window.
        myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        myProcess.StartInfo.CreateNoWindow = True
        myProcess.Start()

        'Waits for Program to finish then exits
        myProcess.WaitForExit()
        myProcess.Close()

    End Sub

    Sub New_Proccess()

        strCopyArguments = String.Empty
        myProcess = New Process()

    End Sub
#End Region

    '***********************************************************************************************************************************
    '  Backup ALL applications to SDCARD
    '***********************************************************************************************************************************
#Region "Backup All Apps"
    Private Sub btnBackupAppsToSDCARD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackupAppsToSDCARD.Click

        Dim strShell, strInstall, strCommands As String
        strShell = " shell "

        Device_State()

        If blnDeviceState = True Then
            MessageBox.Show("All your applications will now be backed up. Please be patient as this will take about 2 minutes to complete.")

            'Install app to backup programs
            New_Proccess()
            strInstall = " install "
            strCommands = "/EMA_Files/_UPLOAD/_Applications/AndUPLite.apk"
            strCopyArguments = strBegCMDProcess & strQuote & strSDKLocation & strADB & strQuote & strInstall & strQuote & strEMA_Location & strCommands & strQuote
            Start_New_CMD()

            'Start the app and backup the files
            New_Proccess()
            strCommands = "am start -a android.intent.action.MAIN -n net.andirc.anduplite/.anduplite"
            strCopyArguments = strBegCMDProcess & strQuote & strSDKLocation & strADB & strQuote & strShell & strCommands
            Start_New_CMD()

            'Delay is needed to make sure that the app has enough time to copy the files
            'Delay(120)

            'CODE ADDED BY JAMEZELLE
            Try
                Dim strWhileDoneTxt As String = " while [ ! -e /sdcard/andirc/backup/app/done.txt ]; do ; done"

                'Wait for backup to finish, then continue
                New_Proccess()
                strCopyArguments = strBegCMDProcess & strQuote & strSDKLocation & strADB & strQuote & strShell & strWhileDoneTxt
                Start_New_CMD()

                'Remove the .apk file from the data section of phone so user cant use it again
                New_Proccess()
                strCommands = "rm data/app/net.andirc.anduplite.apk"
                strCopyArguments = strBegCMDProcess & strQuote & strSDKLocation & strADB & strQuote & strShell & strCommands
                Start_New_CMD()

                'Removes the .apk file from the sdcard of the phone so user cant use it again
                New_Proccess()
                strCommands = "rm sdcard/andirc/backup/app/net.andirc.anduplite.apk"
                strCopyArguments = strBegCMDProcess & strQuote & strSDKLocation & strADB & strQuote & strShell & strCommands
                Start_New_CMD()

                MessageBox.Show("A new folder was created on your SDCARD called \andirc\backup\app. Then all the applications that are installed were copied to that folder. When a message box appears on your phone telling you that the backup was a success, you may continue on.")

            Catch ex As Exception

                Dim strOSname As String = My.Computer.Info.OSFullName
                Dim strPathToApp As String = strEMA_Location & "\" & My.Application.Info.AssemblyName & ".exe"
                Dim strDateTime As String = CStr(DateTime.Now)
                Dim strUserName As String = My.User.Name

                'Code to make an error log
                Filename = "c:\AMA_Error_Log.txt"
                nFileNum = CShort(FreeFile())
                FileClose(nFileNum)
                FileOpen(nFileNum, Filename, OpenMode.Append)
                PrintLine(nFileNum, vbNewLine)
                PrintLine(nFileNum, "*********************************************************************************")
                PrintLine(nFileNum, vbNewLine)
                PrintLine(nFileNum, "Bug Report From: ", strDateTime)
                PrintLine(nFileNum, "Operating System: ", strOSname)
                PrintLine(nFileNum, "Path To App: ", strPathToApp)
                PrintLine(nFileNum, "PC/User Name: ", strUserName)
                PrintLine(nFileNum, vbNewLine)
                PrintLine(nFileNum, "Error Log Report:")
                PrintLine(nFileNum, vbNewLine)
                PrintLine(nFileNum, ex.ToString)
                PrintLine(nFileNum, vbNewLine)
                FileClose(nFileNum)


                'opens file in notepad for user to read this is really only for testing bc the user
                'wont need to see this, we can ask them to open the file if we need whats in it incase the email didnt send
                'path = "C:\WINDOWS\notepad.exe"
                'errorfile = Filename
                'dTaskID = Shell(path & " " & errorfile, AppWinStyle.NormalFocus)

                SendEmail()

                MessageBox.Show("An error occured while backing up your apps. Please make sure your device is plugged in and turned on. Also make sure usb debugging is enabled, and try again.")
            End Try

        ElseIf blnDeviceState = False Then
            MessageBox.Show("Either your device is not plugged in or USB debugging is turned off. Please plug in your device and make sure USB Debugging is enabled on the device and try again.")
        End If

    End Sub
#End Region

    '**********************************************************************************************************************************
    '  Reinstall ALL applications from SDCARD
    '***********************************************************************************************************************************
#Region "Reinstall All Apps"
    Private Sub btnReinstallAllAppsFromSDCARD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReinstallAllAppsFromSDCARD.Click

        Dim strShell, strRemount, strCopyCommands As String

        Device_State()

        If blnDeviceState = True Then

            MessageBox.Show("This will reinstall all the apps that were backed up onto your SDCARD. Can ONLY be used if you used the backup feature in this app. Reinstalling all the applications will take a while, so please be patient.")

            'Copy files to newly created folder
            New_Proccess()
            strRemount = " remount"
            strCopyArguments = strBegCMDProcess & strQuote & strSDKLocation & strADB & strQuote & strRemount
            Start_New_CMD()

            'Reinstalls all apps from sdcard/appsbackup folder
            New_Proccess()
            strShell = " shell "
            strCopyCommands = "busybox install sdcard/andirc/backup/app/*.apk /data/app"
            strCopyArguments = strBegCMDProcess & strQuote & strSDKLocation & strADB & strQuote & strShell & strCopyCommands
            Start_New_CMD()

            MessageBox.Show("All applications have now been reinstalled. It is recommended that you now reboot your phone.")

        End If

    End Sub
#End Region

    '***********************************************************************************************************************************
    '  Get state of device to prevent running commands if no device present - DONE
    '***********************************************************************************************************************************
#Region "Device State"
    Sub Device_State()

        Dim strGetState, strDeviceState, strPrepDevice As String

        'prepares device w/ adb commands
        New_Proccess()
        strGetState = " get-state > "
        strPrepDevice = "\EMA_Files\Prep_Device.txt"
        strCopyArguments = strBegCMDProcess & strQuote & strSDKLocation & strADB & strQuote & strGetState & strQuote & strEMA_Location & strPrepDevice & strQuote
        Start_New_CMD()

        'gets device state
        New_Proccess()
        strDeviceState = "\EMA_Files\Device_State.txt"
        strCopyArguments = strBegCMDProcess & strQuote & strSDKLocation & strADB & strQuote & strGetState & strQuote & strEMA_Location & strDeviceState & strQuote
        Start_New_CMD()

        Try

            Dim sr As StreamReader
            sr = New StreamReader(strEMA_Location & strDeviceState)
            Dim strDeviceStateRL As String

            strDeviceStateRL = sr.ReadLine()
            sr.Close()

            If strDeviceStateRL = "unknown" Then
                MessageBox.Show("Unable to check device state, or you do not have your phone connected. Please check that you have USB Debugging turned on and have followed all directions.")

                blnDeviceState = False
            ElseIf strDeviceStateRL = "device" Then
                blnDeviceState = True
            End If

        Catch ex As Exception

            If MsgBox("We were unable to get device state. Please check to make sure that the phone is connected, USB Debugging turned on, drivers installed for phone, and have followed all directions. If that still doesnt fix the problem, then click YES to submit a bug report to us. Otherwise, click NO and try again. Thank you!", vbYesNo) = vbYes Then

                Dim strOSname As String = My.Computer.Info.OSFullName
                Dim strPathToApp As String = strEMA_Location & "\" & My.Application.Info.AssemblyName & ".exe"
                Dim strDateTime As String = CStr(DateTime.Now)
                Dim strUserName As String = My.User.Name

                'Code to make an error log
                Filename = "c:\AMA_Error_Log.txt"
                nFileNum = CShort(FreeFile())
                FileClose(nFileNum)
                FileOpen(nFileNum, Filename, OpenMode.Append)
                PrintLine(nFileNum, vbNewLine)
                PrintLine(nFileNum, "*********************************************************************************")
                PrintLine(nFileNum, vbNewLine)
                PrintLine(nFileNum, "Bug Report From: ", strDateTime)
                PrintLine(nFileNum, "Operating System: ", strOSname)
                PrintLine(nFileNum, "Path To App: ", strPathToApp)
                PrintLine(nFileNum, "PC/User Name: ", strUserName)
                PrintLine(nFileNum, vbNewLine)
                PrintLine(nFileNum, "Error Log Report:")
                PrintLine(nFileNum, vbNewLine)
                PrintLine(nFileNum, ex.ToString)
                PrintLine(nFileNum, vbNewLine)
                FileClose(nFileNum)


                'opens file in notepad for user to read this is really only for testing bc the user
                'wont need to see this, we can ask them to open the file if we need whats in it incase the email didnt send
                'path = "C:\WINDOWS\notepad.exe"
                'errorfile = Filename
                'dTaskID = Shell(path & " " & errorfile, AppWinStyle.NormalFocus)

                SendEmail()

                MessageBox.Show("Unable to check device state, or you do not have your phone connected. Please check that you have USB Debugging turned on and have followed all directions.")

                blnDeviceState = False

            End If

        End Try

    End Sub
#End Region

    '***********************************************************************************************************************************
    '  Flash new Splash Boot Image to the phone
    '***********************************************************************************************************************************
#Region "Flash Splash Boot Image to phone"

    Private Sub btnFlashSplashImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFlashSplashImage.Click

        getSplashToFlash()

        fastbootSplashImage()

    End Sub

    'Get splash image to put on phone
    Sub getSplashToFlash()

        Dim str1, str2 As String
        Dim blnNoSplashImage As Boolean = False
        Dim OpenfileDialog2 As New OpenFileDialog

        MessageBox.Show("Please select the Splash Image you would like to Flash to your phone. Must be in *.rgb565 format.")

        With OpenfileDialog2

            .CheckFileExists = True
            .ShowReadOnly = True
            .DefaultExt = ".rgb565"

            ' Select the directory on entry.
            str1 = "\EMA_Files\_SPLASHIMAGE"
            str2 = strQuote & strEMA_Location & str1 & strQuote
            .InitialDirectory = str2
            ' Prompt the user with a custom message.
            .Title = "Select the Splash Image you would like to Flash to your phone."
            If .ShowDialog = DialogResult.OK Then
                fileImageToFlash = .FileName
                blnNoSplashImage = False
            Else
                blnNoSplashImage = True
            End If
        End With

    End Sub

    'Splash boot image to phone
    Sub fastbootSplashImage()
        Dim str2, str3, str4 As String

        Device_State()

        If blnDeviceState = True Then
            If blnNoSplashImage = False Then
                Try
                    New_Proccess()
                    str2 = strADB
                    str2 &= " reboot bootloader"
                    strCopyArguments = strBegCMDProcess & str2
                    Start_New_CMD()

                    '10 second delay to wait for phone to enter bootloader mode
                    Delay(10)

                    New_Proccess()
                    str4 = "flash splash1"
                    str2 = fileImageToFlash
                    str3 = " && fastboot reboot "
                    strCopyArguments = strBegCMDProcess & strFastboot & str4 & strQuote & str2 & strQuote & str3
                    Start_New_CMD()

                    MessageBox.Show("Phone now has a new Splash Image.")

                Catch ex As Exception

                    Dim strOSname As String = My.Computer.Info.OSFullName
                    Dim strPathToApp As String = strEMA_Location & "\" & My.Application.Info.AssemblyName & ".exe"
                    Dim strDateTime As String = CStr(DateTime.Now)
                    Dim strUserName As String = My.User.Name

                    'Code to make an error log
                    Filename = "c:\AMA_Error_Log.txt"
                    nFileNum = CShort(FreeFile())
                    FileClose(nFileNum)
                    FileOpen(nFileNum, Filename, OpenMode.Append)
                    PrintLine(nFileNum, vbNewLine)
                    PrintLine(nFileNum, "*********************************************************************************")
                    PrintLine(nFileNum, vbNewLine)
                    PrintLine(nFileNum, "Bug Report From: ", strDateTime)
                    PrintLine(nFileNum, "Operating System: ", strOSname)
                    PrintLine(nFileNum, "Path To App: ", strPathToApp)
                    PrintLine(nFileNum, "PC/User Name: ", strUserName)
                    PrintLine(nFileNum, vbNewLine)
                    PrintLine(nFileNum, "Error Log Report:")
                    PrintLine(nFileNum, vbNewLine)
                    PrintLine(nFileNum, ex.ToString)
                    PrintLine(nFileNum, vbNewLine)
                    FileClose(nFileNum)


                    'opens file in notepad for user to read this is really only for testing bc the user
                    'wont need to see this, we can ask them to open the file if we need whats in it incase the email didnt send
                    'path = "C:\WINDOWS\notepad.exe"
                    'errorfile = Filename
                    'dTaskID = Shell(path & " " & errorfile, AppWinStyle.NormalFocus)

                    SendEmail()

                    MsgBox("Could not reboot into bootloader mode or flash image or reboot phone.")
                End Try
            End If
        End If

    End Sub

#End Region

    '***********************************************************************************************************************************
    '  Add delay in execution of code - DONE
    '***********************************************************************************************************************************
#Region "Delay Application Processes"
    Sub Delay(ByVal dblSecs As Double)

        Const OneSec As Double = 1.0# / (1440.0# * 60.0#)
        Dim dblWaitTil As Date
        Now.AddSeconds(OneSec)
        dblWaitTil = Now.AddSeconds(OneSec).AddSeconds(dblSecs)
        Do Until Now > dblWaitTil
            Application.DoEvents() ' Allow windows messages to be processed
        Loop

    End Sub
#End Region

    '***********************************************************************************************************************************
    ' Backup Applications settings for root users. PAID
    '***********************************************************************************************************************************
#Region "Backup Application Settings"

    Private Sub btnBackupAppSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackupAppSettings.Click

        Device_State()

        If blnDeviceState = True Then
            Try

                Dim strAppSettingsLocation As String = strEMA_Location
                strAppSettingsLocation += "/EMA_Files/EMA_AppSettingsBackup"
                '  MkDir(strAppSettingsLocation)
                Dim strBackupSettingsScripts As String = strEMA_Location & strSDKLocation
                strBackupSettingsScripts += "backupSettings.sh"

                'push script to /data/local and chmod it
                strCopyArguments = strQuote & strEMA_Location & strSDKLocation & strADB & strADBPush
                strCopyArguments += " " & strBackupSettingsScripts & " /data/local && " & strEMA_Location & strSDKLocation & strADB & " shell chmod 755 /data/local/backupSettings.sh" & strQuote

            Catch ex As Exception

                Dim strOSname As String = My.Computer.Info.OSFullName
                Dim strPathToApp As String = strEMA_Location & "\" & My.Application.Info.AssemblyName & ".exe"
                Dim strDateTime As String = CStr(DateTime.Now)
                Dim strUserName As String = My.User.Name

                'Code to make an error log
                Filename = "c:\AMA_Error_Log.txt"
                nFileNum = CShort(FreeFile())
                FileClose(nFileNum)
                FileOpen(nFileNum, Filename, OpenMode.Append)
                PrintLine(nFileNum, vbNewLine)
                PrintLine(nFileNum, "*********************************************************************************")
                PrintLine(nFileNum, vbNewLine)
                PrintLine(nFileNum, "Bug Report From: ", strDateTime)
                PrintLine(nFileNum, "Operating System: ", strOSname)
                PrintLine(nFileNum, "Path To App: ", strPathToApp)
                PrintLine(nFileNum, "PC/User Name: ", strUserName)
                PrintLine(nFileNum, "Rom To Install: ", strWhatRomInstall)
                PrintLine(nFileNum, vbNewLine)
                PrintLine(nFileNum, "Error Log Report:")
                PrintLine(nFileNum, vbNewLine)
                PrintLine(nFileNum, ex.ToString)
                PrintLine(nFileNum, vbNewLine)
                FileClose(nFileNum)


                'opens file in notepad for user to read this is really only for testing bc the user
                'wont need to see this, we can ask them to open the file if we need whats in it incase the email didnt send
                'path = "C:\WINDOWS\notepad.exe"
                'errorfile = Filename
                'dTaskID = Shell(path & " " & errorfile, AppWinStyle.NormalFocus)

                SendEmail()

                MessageBox.Show("Failed to Backup App Settings, make sure device is plugged in and USB Debugging is turned on, on the device and try again.")
            End Try
            'ElseIf blnDeviceState = False Then
            '    MessageBox.Show("Failed to detect device. make sure device is on and plugged into the computer. Also make sure usb debugging is enabled on the device, and try again")
        End If

    End Sub
#End Region

    '***********************************************************************************************************************************
    ' Email Code - DONE
    '***********************************************************************************************************************************
#Region "Send Email"
    Sub SendEmail()

        'create the mail message
        Dim mail As New MailMessage()

        'what file to attach to the email
        Dim attachfile As Attachment = Nothing
        Filename = "c:\AMA_Error_Log.txt"
        attachfile = New Attachment(Filename)

        'set the addresses
        mail.From = New MailAddress("Android@Master.App.com")
        mail.To.Add("log@logfiles.com")

        'set the content
        mail.Subject = "EMA Log File"
        mail.Body = "File is attached."
        'attach the log file
        mail.Attachments.Add(attachfile)
        'smtp server to use
        Dim smtp As New SmtpClient("smtp.gmail.com")

        'to authenticate we set the username and password properites on the SmtpClient
        smtp.Credentials = New NetworkCredential("log@logfiles.com", "password")
        'the port to connect with
        smtp.Port = CInt("587")
        'using SSL encryption
        smtp.EnableSsl = True

        Try
            'sends the email
            smtp.Send(mail)
            'releases the email connection
            smtp.Dispose()
        Catch ex As Exception
            'Logs to file, but doesnt send email
            Dim strOSname As String = My.Computer.Info.OSFullName
            Dim strPathToApp As String = strEMA_Location & "\" & My.Application.Info.AssemblyName & ".exe"
            Dim strDateTime As String = CStr(DateTime.Now)
            Dim strUserName As String = My.User.Name

            'Code to make an error log
            Filename = "c:\AMA_Error_Log.txt"
            nFileNum = CShort(FreeFile())
            FileClose(nFileNum)
            FileOpen(nFileNum, Filename, OpenMode.Append)
            PrintLine(nFileNum, vbNewLine)
            PrintLine(nFileNum, "*********************************************************************************")
            PrintLine(nFileNum, vbNewLine)
            PrintLine(nFileNum, "Bug Report From: ", strDateTime)
            PrintLine(nFileNum, "Operating System: ", strOSname)
            PrintLine(nFileNum, "Path To App: ", strPathToApp)
            PrintLine(nFileNum, "PC/User Name: ", strUserName)
            PrintLine(nFileNum, "Rom To Install: ", strWhatRomInstall)
            PrintLine(nFileNum, vbNewLine)
            PrintLine(nFileNum, "Error Log Report:")
            PrintLine(nFileNum, vbNewLine)
            PrintLine(nFileNum, ex.ToString)
            PrintLine(nFileNum, vbNewLine)
            FileClose(nFileNum)


            'opens file in notepad for user to read this is really only for testing bc the user
            'wont need to see this, we can ask them to open the file if we need whats in it incase the email didnt send
            path = "C:\WINDOWS\notepad.exe"
            errorfile = Filename
            dTaskID = Shell(path & " " & errorfile, AppWinStyle.NormalFocus)

            SendEmail()
        End Try



    End Sub
#End Region

    'edit build.prop
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim sr As StreamReader
        'gets location of app and stores in variable
        'so if user changes location of app, the program
        'is dynamic and changes with the idiot user
        Dim strAppLocation As String = FileSystem.CurDir
        sr = New StreamReader("c:\build.prop")
        Dim strBuildPropModel As String
        Dim strBuildPropArray(19) As String

        'creates array to hold the first 19 lines of build.prop
        For intCount = 0 To 18
            strBuildPropArray(intCount) = sr.ReadLine()
        Next

        sr.ReadLine()

        Dim strRestOfBuildProp As String
        strRestOfBuildProp = sr.ReadToEnd()

        sr.Close()

        Dim strNewBuildPropModel As String = "test"

        'create a string of the new model we want to use, from text box or similar
        strBuildPropModel = "ro.product.model=" & strNewBuildPropModel

        Dim sw As StreamWriter
        sw = New StreamWriter("c:\build.prop")

        'need to write first 19 lines to build.prop to keep same info as before
        For intcount = 0 To 18
            sw.WriteLine(strBuildPropArray(intcount))
        Next

        'write new build.prop model line into file
        sw.WriteLine(strBuildPropModel)

        sw.WriteLine(strRestOfBuildProp)

        sw.Close()

    End Sub
End Class
