Imports System.Windows.Forms

Public Class DialogFolds

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        For Each RB As RadioButton In Me.Controls.OfType(Of RadioButton)()
            'For Each Cont In Me.Controls
            'MessageBox.Show(RB.Name)
            If RB.Checked Then
                bOtv = True
                Form1.LinkLabelSP.Tag = RB.Tag
                Form1.LinkLabelSP.Text = RB.Text
                Exit For
            End If
        Next
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        MessageBox.Show("Папка СП не выбрана. Программа закрывается", "Отмена выбора")
        Application.Exit()
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub LinkLabelGB1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        'Me.Controls.Item("GroupBox1").Controls.Item("LinkLabelGB" & iii).Tag
    End Sub

    Private Sub DialogFolds_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For iii As Integer = 1 To sFoldMass.Length
            Me.Controls.Item("RadioButton" & iii).Visible = True
            Me.Controls.Item("RadioButton" & iii).Tag = sFoldMass(iii - 1)
            Me.Controls.Item("RadioButton" & iii).Text = IO.Path.GetFileName(sFoldMass(iii - 1))
        Next
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged, RadioButton6.CheckedChanged

    End Sub
End Class
