Imports IWshRuntimeLibrary
Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        sPathFoldSP = My.Settings.PathFoldSP & "\" & Format(Now, "yyyy")
        sPathFoldOP = My.Settings.PathFoldOP
        sPathFoldKO = My.Settings.PathFoldKO

        LinkLabelSP.Text = ""
        Me.Height = 125
        'txb_NumSP.
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles txb_NumSP.TextChanged
        Me.AcceptButton = Button1
        If Me.txb_NumSP.Text = "" Then
            Me.Button1.Enabled = False
        Else
            Me.Button1.Enabled = True
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'todo добавить обработку нескольких найденных папок
        'todo создать в параметрах пути папок
        LinkLabelSP.Text = "Производится поиск папки..."
        Me.Button1.Visible = False
        Me.txb_NumSP.Enabled = False
        LinkLabelSP.Text = txb_NumSP.Text
        'LinkLabelSP.Tag = ПоискПапкиWork(sPathFoldSP, "*" & txb_NumSP.Text & "*")
        sFoldMass = IO.Directory.GetDirectories(sPathFoldSP, "*" & txb_NumSP.Text & "*", IO.SearchOption.TopDirectoryOnly)
        If sFoldMass Is Nothing Then
            LinkLabelSP.Text = ПолучениеИмениПапкиWork(sPathFoldSP)
        Else
            Me.Height = 270
            If sFoldMass.Length = 1 Then
                LinkLabelSP.Tag = sFoldMass(0)
                LinkLabelSP.Text = IO.Path.GetFileName(LinkLabelSP.Tag)
            Else
                DialogFolds.ShowDialog()
                'MessageBox(DialogFolds.DialogResult.ToString)
            End If
        End If
        txb_FoldOP.Text = Me.LinkLabelSP.Text
    End Sub

    Private Sub LinkLabelSP_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabelSP.LinkClicked
        Shell("explorer.exe " & Chr(34) & LinkLabelSP.Tag & Chr(34), vbNormalFocus)
    End Sub

    Private Sub txb_NumSP_Enter(sender As Object, e As EventArgs) Handles txb_NumSP.Enter
        If txb_NumSP.Text = "" Then txb_NumSP.Text = "" : Exit Sub
        If txb_NumSP.Text Like "*[!0-9]*" Then txb_NumSP.Text = txb_NumSP.Tag Else : txb_NumSP.Tag = txb_NumSP.Text
        'txb_SPFolder.Text = ПоискПапкиWork("\\Localserver\общие документы\Документация СП\Предзаказ\2018")
        'If txb_SPFolder.Text = "" Then txb_SPFolder.Text = ПолучениеИмениПапкиWork("\\Localserver\общие документы\Документация СП\Предзаказ\2018")
        'txb_SP.Tag = ""
    End Sub

    'Private Sub txb_NumSP_KeyUp(sender As Object, e As KeyEventArgs) Handles txb_NumSP.KeyUp
    '    If txb_NumSP.Text Like "*[!0-9]*" Then txb_NumSP.Text = txb_NumSP.Tag Else txb_NumSP.Tag = txb_NumSP.Text
    '    '    If Not Char.IsDigit(e.KeyValue) Then e.Handled = True
    'End Sub

    Private Sub txb_NumSP_KeyDown(sender As Object, e As KeyEventArgs) Handles txb_NumSP.KeyDown
        If e.KeyData = Keys.Enter Then
            Exit Sub
        End If

        If (e.KeyValue >= 48 And e.KeyValue <= 57) Or (e.KeyValue >= 96 And e.KeyValue <= 105) Or e.KeyData = Keys.Delete Or e.KeyData = Keys.Back Then '48, 57, 96, 105
            e.SuppressKeyPress = False
        Else
            e.SuppressKeyPress = True
        End If
    End Sub
    'MsgBox("", , "")

    '======================= ДОП ФУНКЦИИ =============================================
    '======================= ДОП ФУНКЦИИ =============================================
    Private Function ПолучениеИмениПапкиWork(Optional ByVal InitialPath As String = "", Optional ByVal Title As String = "Выбор папки") As String
        ' функция выводит диалоговое окно выбора папки с заголовком Title,
        ' начиная обзор диска с папки InitialPath
        ' возвращает полный путь к выбранной папке, или пустую строку в случае отказа от выбора


        Dim FBD As New FolderBrowserDialog With {.SelectedPath = Application.StartupPath}
        If FBD.ShowDialog = Windows.Forms.DialogResult.OK Then MsgBox(FBD.SelectedPath)
        If Title = "" Then Title = "Выберите папку"
        FBD.SelectedPath = InitialPath 'Environment.SpecialFolder.ProgramFiles ' Указываем начальную папку(указывать можно только определенные папки(в нашем случае Programm Files))
        If FBD.ShowDialog = DialogResult.OK Then MsgBox(FBD.SelectedPath) '
        ПолучениеИмениПапкиWork = FBD.SelectedPath
        If Not ПолучениеИмениПапкиWork.EndsWith(IO.Path.PathSeparator) Then ПолучениеИмениПапкиWork &= IO.Path.PathSeparator

        'If Not Strings.Right$(ПолучениеИмениПапкиWork, 1) = PS Then ПолучениеИмениПапкиWork = ПолучениеИмениПапкиWork & PS ' если C:\TEMP то C:\TEMP\
    End Function

    Private Function ExistDir(ByVal DirName As String) As Boolean ' проверка существования папки
        ExistDir = False 'Считаем, что пока что не существует
        ExistDir = (Dir(DirName, vbDirectory) <> "") 'Собственно, ответ на интересующий вопрос
    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        MessageBox.Show("в ОП")
    End Sub

    '\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
    Private Sub rm_Load()
        Dim FileDir As String = FileIO.FileSystem.GetFileInfo(LinkLabelSP.Tag).DirectoryName
        Dim WshShell = CreateObject("WScript.Shell")
        'Dim strDesktop = WshShell.SpecialFolders("Desktop") 'переменная strDesktop будет равна пути до рабочего стола будь то путь в windows 7 (C:\Users\Имя пользователя\Desktop) или в windows xp (C:\Documents and settings\Имя пользователя\Рабочий стол)
        'Dim oShellLink = WshShell.CreateShortcut(strDesktop & "\название ярлыка.lnk")  'кто не знает то у ярлыков формат lnk
        Dim strDesktop = "\\Localserver\общие документы\Документация СП\Предзаказ\2018\СП-1319,Сарметаллстрой" 'переменная strDesktop будет равна пути до рабочего стола будь то путь в windows 7 (C:\Users\Имя пользователя\Desktop) или в windows xp (C:\Documents and settings\Имя пользователя\Рабочий стол)
        Dim oShellLink = WshShell.CreateShortcut(FileDir & "\название ярлыка.lnk")  'кто не знает то у ярлыков формат lnk
        oShellLink.TargetPath = "\\Localserver\общие документы\Документация СП\Распоряжение от ОП"
        oShellLink.WindowStyle = 1
        oShellLink.IconLocation = "путь к картинке которая будет на ярлыке"
        oShellLink.Description = "подсказка которая будет отображатся при наведении на курсор"
        oShellLink.WorkingDirectory = "путь к рабочей папке в которой лежит exe"
        oShellLink.Save 'создать ярлык
    End Sub
    Sub CreateShortcut()
        'slPath = IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), Application.ExecutablePath)
        'Dim shell As New WshShell
        'Dim link As IWshShortcut = shell.CreateShortcut(slPath)
        'link.TargetPath = Application.ExecutablePath
        'link.WorkingDirectory = Application.StartupPath
        'link.Save()
    End Sub



    'Private Function ПоискПапкиWork(Optional sFold As String = "", Optional sMask As String = "*") As String
    '    LinkLabelSP.Text = "Производится поиск папки..."
    '    'MsgBox(FolderSelectDialog.Show(Me.Handle, "C:\", "Выберите папку").FileName)
    '    sFoldMass = IO.Directory.GetDirectories(sFold, sMask, IO.SearchOption.TopDirectoryOnly)
    '    If sFoldMass.Length > 1 Then
    '        DialogFolds.Show()
    '        'Return ВыборОднойПапки(sFoldMass)
    '    Else
    '        Return sFoldMass(0)
    '    End If
    'End Function

    'Private Function ВыборОднойПапки(sFoldMass())
    '    '15; 100
    '    Dim iii As Integer
    '    Me.GroupBox1.Location = New Point(15, 100)
    '    For iii = 1 To sFoldMass.Length
    '        Me.Controls.Item("GroupBox1").Controls.Item("LinkLabelGB" & iii).Visible = True
    '        Me.Controls.Item("GroupBox1").Controls.Item("LinkLabelGB" & iii).Tag = sFoldMass(iii - 1)
    '        Me.Controls.Item("GroupBox1").Controls.Item("LinkLabelGB" & iii).Text = IO.Path.GetFileName(sFoldMass(iii - 1))
    '    Next 'cel
    'End Function

End Class
