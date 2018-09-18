Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LinkLabelSP.Text = ""
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles txb_NumSP.TextChanged
        If Me.txb_NumSP.Text = "" Then
            Me.Button1.Enabled = False
        Else
            'If IsNumeric(sender.text) Then
            '    Me.txb_NumSP.Tag = Me.txb_NumSP.Text
            'Else
            '    Me.txb_NumSP.Text = Me.txb_NumSP.Tag
            'End If

            Me.Button1.Enabled = True
            End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'todo добавить поиск папки по номеру СП
        'todo добавить обработку нескольких найденных папок
        LinkLabelSP.Text = txb_NumSP.Text
        LinkLabelSP.Tag = "E:\Работа\Спецзаказы\!Стандартные\ТП АСН-100-09-Э-03.78 НЖ"
        'todo создать в параметрах пути папок
        LinkLabelSP.Text = ПолучениеИмениПапкиWork()
    End Sub

    Private Sub LinkLabelSP_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabelSP.LinkClicked
        Shell("explorer.exe " & LinkLabelSP.Tag, vbNormalFocus)
    End Sub

    Private Sub txb_NumSP_Enter(sender As Object, e As EventArgs) Handles txb_NumSP.Enter
        If txb_NumSP.Text = "" Then txb_NumSP.Text = "" : Exit Sub
        'If txb_SP.Text Like "*[!0-9]*" Then txb_SP.Text = txb_SP.Tag Else : txb_SP.Tag = txb_SP.Text
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
        If Title = "" Then Title = "Выберите папку"
        'Dim PS As String : PS = PathSeparator
        FBD.RootFolder = Environment.SpecialFolder.ProgramFiles ' Указываем начальную папку(указывать можно только определенные папки(в нашем случае Programm Files))
        If FBD.ShowDialog = DialogResult.OK Then MsgBox(FBD.SelectedPath) '
        ПолучениеИмениПапкиWork = FBD.SelectedPath
        If Not ПолучениеИмениПапкиWork.EndsWith(IO.Path.PathSeparator) Then ПолучениеИмениПапкиWork &= IO.Path.PathSeparator

        'If Not Strings.Right$(ПолучениеИмениПапкиWork, 1) = PS Then ПолучениеИмениПапкиWork = ПолучениеИмениПапкиWork & PS ' если C:\TEMP то C:\TEMP\
    End Function

    Private Function ExistDir(ByVal DirName As String) As Boolean ' проверка существования папки
        ExistDir = False 'Считаем, что пока что не существует
        ExistDir = (Dir(DirName, vbDirectory) <> "") 'Собственно, ответ на интересующий вопрос
    End Function

    'Private Function ПоискПапкиWork(sFold As String = "") As String
    '    LinkLabelSP.Text = "Производится поиск папки..."
    '    Dim FSO, SubFolder, sFolds, tsOut, fFolder, iii
    '    iii = -1
    '    FSO = CreateObject("Scripting.FileSystemObject")   'Создаем объект FileSystemObject
    '    fFolder = FSO.GetFolder(sFold)   'Путь к корневому каталогу
    '    For Each SubFolder In fFolder.SubFolders   'Цикл по всем подкаталогам
    '        If InStr(SubFolder.Name, txb_SP.Text) <> 0 Then
    '            iii = iii + 1 : DoEvents
    '            If iii = 0 Then ReDim NajdennyePapki(iii) Else ReDim Preserve NajdennyePapki(iii)
    '            NajdennyePapki(iii) = SubFolder.Name
    '        End If
    '    Next
    '    '   MsgBox (333)
    '    If iii > -1 Then
    '        Dim f As Object, S, jjj, Star6ajaData, Star6ijNomer
    '        Star6ajaData = 0 : Star6ijNomer = 0
    '        For jjj = 0 To iii
    '     Set f = FSO.GetFolder(sFold & "\" & NajdennyePapki(jjj))
    '     S = f.DateCreated : DoEvents
    '            If S > Star6ajaData Or Star6ajaData = 0 Then
    '                Star6ajaData = S : Star6ijNomer = jjj
    '            End If
    '        Next
    '        ПоискПапкиWork = NajdennyePapki(Star6ijNomer)
    '    End If
    'End Function


End Class
