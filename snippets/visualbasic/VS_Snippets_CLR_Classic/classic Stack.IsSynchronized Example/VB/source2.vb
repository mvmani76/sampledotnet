﻿Imports System.Collections

Public Class SamplesStack
    Public Shared Sub Main()
        ' <Snippet2>
        Dim myCollection As New Stack()

        SyncLock myCollection.SyncRoot
            For Each item As Object In myCollection
                ' Insert your code here.
            Next item
        End SyncLock
        ' </Snippet2>
    End Sub
End Class

