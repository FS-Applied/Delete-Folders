
Imports System.Text


Public Class Form1


    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        'Exit app
        '
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to exit?", "Applied Systems", MessageBoxButtons.YesNo)
        If result = DialogResult.Yes Then
            Close()
        End If

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        'Check for items in the Data Grid
        If DataGridView1.Rows.Count = 0 Then
            MsgBox("Oops, did you forget something? I bet you did!!!", vbCritical, "Applied Systems")
            Exit Sub
        End If

        'Proceed
        '
        Dim result As DialogResult = MessageBox.Show("All sub-folders and files inside those folders will be deleted. Are you sure you want to proceed?", "Applied Systems", MessageBoxButtons.YesNo)

        If result = DialogResult.Yes Then

            Dim strFolderFromList As String

            For Each row As DataGridViewRow In DataGridView1.Rows

                strFolderFromList = row.Cells(1).Value

                Try
                    'Does the magic
                    '
                    My.Computer.FileSystem.DeleteDirectory(strFolderFromList,
                                                            Microsoft.VisualBasic.FileIO.UIOption.AllDialogs,
                                                            Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin)

                    'Add Status to the GridView
                    '
                    DataGridView1(0, row.Index).Value = "Deleted"
                    DataGridView1.Refresh()

                Catch ex As Exception

                    If ex.Message.Contains("Could not find directory") Then
                        'Folder not found
                        DataGridView1(0, row.Index).Value = "Not found"
                        DataGridView1.Refresh()
                    Else
                        'Other
                        DataGridView1(0, row.Index).Value = ex.Message
                        DataGridView1.Refresh()
                    End If

                End Try

            Next

        Else
            'Does nothing and
            'ends the routine
            '
            Exit Sub
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        ' Call ShowDialog.
        Dim result As DialogResult = OpenFileDialog1.ShowDialog()

        ' Test result.
        If result = Windows.Forms.DialogResult.OK Then

            ' Get the file name.
            Dim path As String = OpenFileDialog1.FileName
            Dim TextLine As String

            Try

                If System.IO.File.Exists(path) = True Then
                    Using objReader As New System.IO.StreamReader(path, Encoding.ASCII)
                        Do While objReader.Peek() <> -1
                            TextLine = objReader.ReadLine()
                            'SplitLine = Split(TextLine, ";")
                            Me.DataGridView1.Rows.Add("", TextLine) ' SplitLine)
                        Loop
                    End Using
                Else
                    MsgBox("File does not exist.")
                End If

            Catch ex As Exception
                ' Strange error
                MsgBox(ex.Message, vbCritical, "Applied Systems")
                Exit Sub
            End Try

        End If

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

        MsgBox("Applied Systems 2023 - FS" & vbCrLf & "Version 1.0", vbInformation, "About")

    End Sub
End Class
