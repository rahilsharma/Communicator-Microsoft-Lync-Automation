Imports System
Imports System.Runtime.InteropServices
Imports CommunicatorAPI
Public Class Form1
    Dim comm_rs As New CommunicatorAPI.Messenger

    'Dim comm As CommunicatorAPI.IMessengerContacts = New CommunicatorAPI.IMessengerContacts
    Sub rs()
        Try
            If (comm_rs.MyStatus <> MISTATUS.MISTATUS_ONLINE) Then
                comm_rs.AutoSignin()
            End If
            Dim mesg As String
            Dim name, stat, sign_name
            Dim comm As IMessengerContacts
            comm = comm_rs.MyContacts
            Dim rs As IMessengerContact
            Dim inss As Integer = comm.Count
            Dim i As Integer = 0
            For i = 0 To inss - 1
                rs = comm.Item(i)
                name = rs.FriendlyName
                'MsgBox(name.ToString)
                ' phone = rs.PhoneNumber(MPHONE_TYPE.MPHONE_TYPE_WORK)
                '"  Phone no. throwing some exception :" & phone 
                stat = rs.Status
                sign_name = rs.SigninName
                If stat = 0 Then
                    mesg = "Unknown"
                ElseIf stat = 1 Then
                    mesg = "offline"
                ElseIf stat = 2 Then
                    mesg = "online"
                ElseIf stat = 6 Then
                    mesg = "invisible"
                ElseIf stat = 10 Then
                    mesg = "busy"
                ElseIf stat = 14 Then
                    mesg = "be right back"
                ElseIf stat = 18 Then
                    mesg = "idle"
                ElseIf stat = 34 Then
                    mesg = "away"
                ElseIf stat = 82 Then
                    mesg = "meetiung"
                Else
                    mesg = "not seen every stat code .,"
                End If
                ListBox1.Items.Add("Friendly name :" & name & "  status : " & stat & " : " & mesg & "  sign_name : " & sign_name)

            Next

        Catch ex As Exception

        End Try

    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ListBox1.Items.Clear()

        rs()
    End Sub
    Dim counts As Integer

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged

        Dim name1
        counts = ListBox1.SelectedIndex
        Dim comm1 As IMessengerContacts
        comm1 = comm_rs.MyContacts
        Dim rs33 As IMessengerContact
        rs33 = comm1.Item(counts)
        name1 = rs33.SigninName
        TextBox4.Text = name1
        kk()

    End Sub



    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        '' Dim signin As String = TextBox4.Text
        ' 'MsgBox(signin)
        ' Dim sipUris As New Object
        ' sipUris = {signin}

        Dim comm1 As IMessengerContacts
        comm1 = comm_rs.MyContacts
        Dim rs33 As IMessengerContact
        rs33 = comm1.Item(counts)


        ' Dim communicator As CommunicatorAPI.IMessengerAdvanced
        Dim msgrAdv As CommunicatorAPI.IMessengerAdvanced = comm_rs

        ' comm_rs.OnIMWindowCreated += New DMessengerEvents_OnIMWindowCreatedEventHandler(AddressOf OnIMWindowCreated)


        Dim obj As Object

        'MsgBox("start conversation window throwing some error......will see afterwards")

        'communicator.OnIMWindowCreated += New DMessengerEvents_OnIMWindowCreatedEventHandler(communicator_OnIMWindowCreated)
        'dafuq we needed to enter the signin name ...........wtf wtf
        obj = msgrAdv.StartConversation(CONVERSATION_TYPE.CONVERSATION_TYPE_IM, rs33.SigninName, Nothing, "Testing", "2", Nothing)
        'type casting error here wtf...................................

        'win_handle = obj



        'Dim mussage = "hi"


        '' win_handle.HWND = obj.ToString
        Dim number As Integer

        Dim i As Integer



        Dim mussage

        If TextBox2.Text.ToString = "no. of times" Then
            number = 1
        Else
            number = Int(TextBox2.Text)

        End If
        If TextBox1.Text.ToString = "message" Then
            MsgBox("nothing entered")
            mussage = "message"
        Else
            mussage = (TextBox1.Text)


        End If
        For i = 0 To number - 1
            SendKeys.Send(mussage)

            SendKeys.Send("{ENTER}")

        Next



        'obj.sendtext(mussage)
    End Sub

    Dim imWindow
    '    Sub OnIMWindowCreated(ByVal pIMWindow As IMessengerConversationWndAdvanced)



    '        imWindow = pIMWindow
    '        SendTextMessage("Hi. You received this message as part of testing the OC API.")



    '    End Sub

    '    Sub SendTextMessage(ByVal msg As String)
    '{
    '        If imWindow = Nothing Then
    '            Return
    '        End If

    '        If imWindow.HWND = imWindowHandle Then

    '        End If
    '  {
    '     imWindow.SendText(msg);
    '  }
    '}




    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        MsgBox("will do afterwards ....need to see how to store the info first")
    End Sub
    Dim ch As String
    Dim selected_item
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click



        'If CheckedListBox1.CheckedItems.Count > 1 Then
        '    MsgBox("check only one value")
        '    GoTo x
        'End If

        'For Each items In CheckedListBox1.CheckedItems
        '    ch = items
        'Next
        If ListBox2.SelectedItems.Count < 1 Then
            MsgBox("select any status first")
            GoTo x
        Else
            selected_item = ListBox2.SelectedItem.ToString()
        End If

        Dim x As Double = TextBox6.Text

        change_status(selected_item)
        Timer1.Interval = x * 1000
        Timer1.Enabled = True
        Me.WindowState = FormWindowState.Minimized
        GC.Collect()
x:

    End Sub


    'for calling whole function of timer tick again

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        change_status(selected_item)
        GC.Collect()
    End Sub



    Sub change_status(ByVal selected_item)

        Dim a_contact As IMessengerContactAdvanced = comm_rs.GetContact(comm_rs.MySigninName.ToString, comm_rs.MyServiceId.ToString)
        'Dim mpp As ArrayList

        'mpp = Nothing

        'mpp(PRESENCE_PROPERTY.PRESENCE_PROP_AVAILABILITY) = 6500
        'a_contact.PresenceProperties = mpp

        'mpp(PRESENCE_PROPERTY.PRESENCE_PROP_MSTATE) = MISTATUS.MISTATUS_BUSY
        'a_contact.PresenceProperties = mpp
        'to check if already set to away

        'Dim str1 As Array

        'str1 = a_contact.PresenceProperties
        'MsgBox(str1(0) & "  " & str1(1))
        If selected_item = "available" Then
            Dim str1 As Array
            Dim str2 As String
            str1 = a_contact.PresenceProperties
            str2 = str1(0) & str1(1)
            Dim str3 = "23000"
            If Not str2.Contains(str3) Then
                Dim str(2)
                str(PRESENCE_PROPERTY.PRESENCE_PROP_AVAILABILITY) = 2
                a_contact.PresenceProperties = (str)
                str(PRESENCE_PROPERTY.PRESENCE_PROP_MSTATE) = MISTATUS.MISTATUS_ONLINE
                'str(PRESENCE_PROPERTY.PRESENCE_PROP_MSTATE) = MISTATUS.MISTATUS_BUSY
                a_contact.PresenceProperties = (str)
                GC.Collect()
            End If
        End If

        If selected_item = "donotdisturb" Then
            Dim str1 As Array
            Dim str2 As String
            str1 = a_contact.PresenceProperties
            str2 = str1(0) & str1(1)
            Dim str3 = "1149000"
            If Not str2.Contains(str3) Then
                Dim str(2)
                str(PRESENCE_PROPERTY.PRESENCE_PROP_AVAILABILITY) = 2
                a_contact.PresenceProperties = (str)
                str(PRESENCE_PROPERTY.PRESENCE_PROP_MSTATE) = MISTATUS.MISTATUS_DO_NOT_DISTURB

                a_contact.PresenceProperties = (str)
                GC.Collect()
            End If
        End If
        If selected_item = "busy" Then
            Dim str1 As Array
            Dim str2 As String
            str1 = a_contact.PresenceProperties
            str2 = str1(0) & str1(1)
            Dim str3 = "106000"
            If Not str2.Contains(str3) Then
                Dim str(2)
                str(PRESENCE_PROPERTY.PRESENCE_PROP_AVAILABILITY) = 2
                a_contact.PresenceProperties = (str)
                'str(PRESENCE_PROPERTY.PRESENCE_PROP_MSTATE) = MISTATUS.MISTATUS_ONLINE
                str(PRESENCE_PROPERTY.PRESENCE_PROP_MSTATE) = MISTATUS.MISTATUS_BUSY
                a_contact.PresenceProperties = (str)
                GC.Collect()
            End If
        End If
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim rs12
        rs12 = comm_rs.MyFriendlyName()
        Label1.Text = rs12.ToString
        rs()
    End Sub

    'Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
    '    Form2.Show()
    'End Sub
    Dim flags As Integer = 0
    Public Sub kk()
        Try


            Dim phone
            counts = ListBox1.SelectedIndex
            Dim comm1 As IMessengerContacts
            comm1 = comm_rs.MyContacts
            Dim rs33 As IMessengerContact
            rs33 = comm1.Item(counts)



            'MsgBox(name.ToString)
            phone = rs33.PhoneNumber(MPHONE_TYPE.MPHONE_TYPE_WORK)
            Label3.Text = phone.ToString
        Catch ex As Exception
            Label3.Text = ex.Message
            flags = 1
        End Try
        If flags = 1 Then
            Try
                Dim phone
                counts = ListBox1.SelectedIndex
                Dim comm1 As IMessengerContacts
                comm1 = comm_rs.MyContacts
                Dim rs33 As IMessengerContact
                rs33 = comm1.Item(counts)



                'MsgBox(name.ToString)
                phone = rs33.PhoneNumber(MPHONE_TYPE.MPHONE_TYPE_HOME)
                Label3.Text = phone.ToString
            Catch ex As Exception
                Label3.Text = ex.Message
                flags = 2
            End Try
        End If
        If flags = 2 Then
            Try
                Dim phone
                counts = ListBox1.SelectedIndex
                Dim comm1 As IMessengerContacts
                comm1 = comm_rs.MyContacts
                Dim rs33 As IMessengerContact
                rs33 = comm1.Item(counts)



                'MsgBox(name.ToString)
                phone = rs33.PhoneNumber(MPHONE_TYPE.MPHONE_TYPE_MOBILE)
                Label3.Text = phone.ToString
            Catch ex As Exception
                Label3.Text = ex.Message
                flags = 3
            End Try
        End If
        If flags = 3 Then
            Try
                Dim phone
                counts = ListBox1.SelectedIndex
                Dim comm1 As IMessengerContacts
                comm1 = comm_rs.MyContacts
                Dim rs33 As IMessengerContact
                rs33 = comm1.Item(counts)



                'MsgBox(name.ToString)
                phone = rs33.PhoneNumber(MPHONE_TYPE.MPHONE_TYPE_ALL)
                Label3.Text = phone.ToString
            Catch ex As Exception
                Label3.Text = ex.Message
                flags = 4
            End Try
        End If
        If flags = 4 Then
            Try
                Dim phone
                counts = ListBox1.SelectedIndex
                Dim comm1 As IMessengerContacts
                comm1 = comm_rs.MyContacts
                Dim rs33 As IMessengerContact
                rs33 = comm1.Item(counts)



                'MsgBox(name.ToString)
                phone = rs33.PhoneNumber(MPHONE_TYPE.MPHONE_TYPE_CUSTOM)
                Label3.Text = phone.ToString
            Catch ex As Exception
                Label3.Text = ex.Message
                flags = 5
            End Try
        End If
    End Sub



End Class