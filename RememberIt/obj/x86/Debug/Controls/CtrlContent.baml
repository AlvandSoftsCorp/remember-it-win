   M S B A M L   `   `   `  ����  
RememberIt    RememberIt.CtrlContent   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        � 	Resources  5	         PasteCmd �� Text��%)   &	        �� 5
   
   $ Paste��6+   5       J� CommandBindings 5      )  #System.Windows.Input.CommandBinding  5   
   -   5           Command# [  5      6=    J� InputBindings 5      �� 5   
    �� Command# [  6   
 �� Key$ V��69    �� 	Modifiers$
 Ctrlf�6A    �� CommandParameter$ Paste��6R   .��5      � 	 � RowDefinitions	 5   
   �� 5      $��30 �6   �� 5      $��* �6   �� 5      $��60 �6   .+�5   
   � $��0��6   
 � ColumnDefinitions
 5      �� 5      $��* �6#   �� 5       $��* �6#   �� 5!      $��* �6#   5#      �� 5%   
   -   5        $��scScroll��5%      $��1��6'   .��5&      � -   5        $��
grdContent��5&       J� 	AllowDrop . 
 5'      �� 5(      $
��Auto �6'   �� 5)      $��* �6'   5+      5,      � 5.   
   $��2��6   
����ح�
 5/      �� 50      $
��Auto �6#   �� 51      $��* �6#   .+�53      �� -   5        $��txtAdd��53       �� AcceptsReturn . $��1��69   $��Tahoma)�6I   $��Right=�6]   ���	#FFCEDEAB$��18(�6�    �� TextAlignment$ Right=�6�   .��6�    54      �� 55      # [  6!   $ V��6E   $
 Ctrlf�6M   $ Paste��6^   .��56   )   57      �� 59      -   5        $��btnAdd��59      $��0��6#   .��5:      �� -   5         ��Name$
 pnl2��5:       �� Orientation$ 
Horizontal=�6+   .+�5;      �� -   5        $��imgAddContent��5;      $��/pic/mic_64.png��61   $��50��6J   $��50��6U   $
��Left=�6a   �� 5<      $�� ��6    $��Center=�6(   5=      5>      5A      5B      5E      