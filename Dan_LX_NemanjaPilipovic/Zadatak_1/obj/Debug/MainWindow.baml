   M S B A M L   `   `   `  ����D  @Zadatak_1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Zadatak_1	Zadatak_1  5   	       Zadatak_1.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:Zadatak_1     5   	     � Title$  	Main View��5   	   $	��450��6   $
��1300��6(   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $��20 �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��20 �6    � RowDefinitions 5   
   �� 5      $��20 �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��20 �6   .+�5!   
   � $��1��6   $��3��6   $
��auto��6,   $
��auto��6:   .+�5"      &   System.Windows.Controls.DataGrid  -   5          Name$ AllInfoAboutWorker��5"         AutoGenerateColumns .     
IsReadOnly .    HeadersVisibility$ Column=�5#   )   ��5"   3   �� *AllInfoAboutWorker+ �� UpdateSourceTrigger$ PropertyChanged=� �� SelectedItem 5#   D   �� *
ViewWorker+$ PropertyChanged=�	   Columns	 5%      0  *System.Windows.Controls.DataGridTextColumn  5&      ,  &System.Windows.Controls.DataGridColumn
   Header$

 Name��6)   1  +System.Windows.Controls.DataGridBoundColumn   Binding 67   �� *Name+  5'      $
 Surname��6)    6:   �� *	Surname+  5(      $
 Date of Birth��6)    6@   �� *DateOfBirth+  5)      $
 ID Card Number��6)    6A   �� *IDCardNumber+  5*      $

 JMBG��6)    67   �� *JMBG+  5+      $
 Geneder��6)    6:   �� *Gender+  5,      $
 Phone Number��6)    6?   �� *PhoneNumber+  5-      $
 Sector��6)    69   �� *Sector+  5.      $
 Address��6)    6:   �� *	Address+  5/      $

 City��6)    67   �� *City+  50      $
 Country��6)    6:   �� *	Country+  51      $
 Address��6)    6:   �� *	Address+  52      $
 Manager��6)    6:   �� *	FKManager+4  .System.Windows.Controls.DataGridTemplateColumn  53         CellTemplate 54      �� 55      .R�56   "   �� $��	btnDelete��6)   $��Delete��6:   $��4q�59   %   $
��Bold"�60   ��57   %   �� *DataContext.Delete+$ PropertyChanged=� �� RelativeSource �� *FindAncestor+ �� AncestorType" �59   0   5:      5<        5=       5>      �� 5?      .R�5@   "   �� $��	btnUpdate��6)   $��Update��6:   $��4q�5C   %   $
��Bold"�60   ��5A   %   �� *DataContext.Update+$ PropertyChanged=� �� *FindAncestor+" �5C   0   5D      5F      5H      5I      �� 5J   
   -   5        $��btnAddWorker��5J   .   $��1��6   $��4��6!   $��
Add Worker��6B   $��30��6W   $	��200��6c   $��5q�6o   $
��Left=�5K      $
��Bold"�6s   ��60   �� *	AddWorker+$ PropertyChanged=�6s   5L      5M      