   M S B A M L   `   `   `  ����E  ARememberIt, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:RememberIt
RememberIt  5           RememberIt.CtrlCardBrowser   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      'myclr-namespace:RememberIt     5        � 	Resources  5
         CmdKeyBoard �� Text��%*   &	        �� 5   
   $ KeyCmd��6.   5       J� CommandBindings 5      )  #System.Windows.Input.CommandBinding  5   
   -   5           Command# [  5      6@    J� InputBindings 5      �� 5   
    �� Command# [  6    �� Gesture$ 	ALT+RIGHT��6<    �� CommandParameter$ NextCard��6P   �� 5   
   # [  6   $ ALT+LEFT��6<   $ PrevCard��6O   �� 5   
   # [  6   $ ALT+UP��6<   $ Up��6M   �� 5   
   # [  6   
 �� Key$
 DOWN��6<   	 �� 	Modifiers$		 ALTf�6H   $
 Down��6X   .��5      � 
 � RowDefinitions
 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $��40 �6   �� 5      $��30 �6    � ColumnDefinitions 5"   
   �� 5#      $��* �6   .+�5&   
   � $��0��6   $��0��6    5'      �� 5(      $��* �6#   �� 5)      $��* �6#   .+�5+      �� -   5         ��Name$ lblCardNumber��5,      $��0��5-      $��1��5.      $��Title��5/      ���	#FF77934B$	��0,0q�51      
���  ��$
��Bold"�53      $��Right=�54      $��Center=�55      $��Stretch=�56      $��Stretch=�57      �� 5:      -   5        $ lblTitle��5;      $��0��5<      $��0��5=      $��Title��5>      ���	#FF77934B$	��0,0q�5@      
���  ��$
��Bold"�5B      $
��Left=�5C      $��Center=�5D      $��Stretch=�5E      $��Stretch=�5F      5H         RememberIt.CtrlContent  5J   
   -   5          Name$ ctrlContent��5L      $��1��5K      $��Stretch=�5M      $��Stretch=�5N      $	��0,0q�5O      $y�Visible=�5P      �� 5S   
   -   5        $ lblInfo��5T      $��3��5U      $��0��5V      $#��By: Parszadeh.Ahmad@gmail.com��5W      ���	#FF77934B$	��0,0q�5Y      
����  �$
��Bold"�5[      $��Center=�5\      $��Center=�5]      $��Stretch=�5^      $��Stretch=�5_      � 5b   
   -   5         �Name$ 
grdButtons��5e      $��2��5c      $��0��5d      $y�Hidden=�5f      
 5h      �� 5i      $
��Auto �6     5k      �� 5l      $��* �6#   �� 5m      $��* �6#   .+�5o      �� -   5         ��Name$ btnCorrectAnswer��5o      $��2��6/   $��0��6<   .��5p      �� -   5         ��Name$
 pnl1��5p       �� Orientation$ 
Horizontal=�6+   .+�5q      �� $��/pic/true_answer.png��6   $��40��6:   $��40��6E   $
��Left=�6Q   �� 5r      $�� ��6    $��Center=�6(   5s      5t      �� 5u      -	   5        $ btnWrongAnswer��5u      $��2��6-   $��1��6:   .��5v      �� -
   5        $
 pnl2��5v      $ 
Horizontal=�6+   .+�5w      �� $��/pic/false_answer.png��6   $��40��6;   $��40��6F   $
��Left=�6R   �� 5x      $�� ��6    $��Center=�6(   5y      5z      5{      5}      5~      