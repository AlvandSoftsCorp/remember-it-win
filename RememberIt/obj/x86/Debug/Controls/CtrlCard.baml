   M S B A M L   `   `   `  ����E  ARememberIt, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:RememberIt
RememberIt  5   7       RememberIt.CtrlCard   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      'myclr-namespace:RememberIt     5   7   -   5        .��5
      �   � RowDefinitions  5   
   �� 5      $��40 �6   �� 5      $��* �6   �� 5      $��40 �6   .+�5   
   �� -   5        $��lblDefaultCardGroupName��5      $��0��6/   $��18(�6<   ���	#FF2230CE���	#FFE2C2C26a   �� 5   
   -   5        $��tcCard��5   @   $��1��6   $��Stretch=�6"   $��Stretch=�6N   .@�5      �� -   5        $��
tiQuestion��5   (   $��Question��6   .��5      � .+�5         RememberIt.CtrlContent  -   5          Name$ ctrlQuestion��5   C   $��Stretch=�6%   $��Stretch=�6Y   5      5      �� 5      -   5        $��tiAnswer��5   &   $��Answer��6   .��5      � .+�5        -   5        $ 
ctrlAnswer��5   C   $��Stretch=�6%   $��Stretch=�6W   5      5      �� 5      -   5        $��
tiReminder��5   (   $��Reminder��6   .��5      � .+�5        -	   5        $ ctrlReminder��5   C   $��Stretch=�6%   $��Stretch=�6Y   5       5!      5"      � 5$   
   $��2��6    � ColumnDefinitions 5%      �� 5&      $��* �6#   �� 5'      $��* �6#   .+�5)      �� -
   5        $��btnSave��5)   2   $��0��6   $��0��6"   $
��Save��6A   $
��Bold"�6f   �� 5*      -   5        $��
btnDiscard��5*   2   $��0��6   $��1��6"   $��Discard��6D   $
��Bold"�6o   5+      5-      5.      