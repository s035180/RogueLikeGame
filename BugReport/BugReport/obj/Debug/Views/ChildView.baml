   M S B A M L   `   `   `  ����D  @BugReport, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:BugReport.ViewsBugReport.Views  5   	       BugReport.Views.ChildView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:BugReport.Views     5   	     � Title$  	ChildView��5   	   $	��600��6   $	��600��6(   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $��20 �6   �� 5      $	��100 �6   �� 5      $	��100 �6   �� 5      $
��auto �6   �� 5      $��20 �6   �� 5      $	��100 �6   �� 5      $��* �6   �� 5      $��20 �6    � RowDefinitions 5   
   �� 5      $��20 �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��20 �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��20 �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5       $��* �6   �� 5!      $��20 �6   .+�5$   
   �� $��1��6   $��2��5%      $��3��5&      $��4q�5'      $��Center=�5(         Header1#��[  5)      Advice to improve the game6Q   �� 5+   
   $��4��6   $��1��5,      $��2��5-      $��4q�5.      $
��Left=�5/      Game Revision:6?   �� 51   
   -   5         ��Name$ Revision��55      $��5��51      $��1��52      $��2��53      $
��Left=�54      $	��150��58       �� SelectedValue 56      ��  �� Path$ SelectedRevision� �� Mode$ OneWay=� �� SelectedItem 57      �� $ SelectedRevision�$ OneWayToSource=�58   !   �� 5:   
   $��7��6   $��1��5;      $��2��5<      $��4q�5=      $
��Left=�5>      Description:6=   �� 5@   
   -   5         ��Name$ AdviceDescription��5E      $��1��5@      $��8��5A      $��2��5B      $��4q�5C      $	��100��5D      $
��Left=�5F      $	��150��5G      	 �� TextWrapping$
	 Wrap=�5H      
 �� AcceptsReturn
 . " �� VerticalScrollBarVisibility$ Visible=�5J      �� 5L   
   -   5         ��Name$ Return��5O      $��10��5L      $��4��5M      $��10q�5N        roundbutton#��[ 5P      $��65��5Q      $��65��5R      .��6   Return6%   �� 5T   
   -   5        $
 Save��5W      $��10��5T      $��5��5U      $��10q�5V      #��[ 5X      $��65��5Y      $��65��5Z      .��6   Save6#   �� 5\   
   $��7��6   $��3��5]      $��2��5^      $��4q�5_      $��
Game Mark:��5`      $��Right=�5a      62   �� 5c   
   $��7��6   $��5��5d      $��4q�5e      $
��Left=�5g      ��5f      �� $
 Mark�$ OneWay=�5g   1   �� 5i   
   -   5         ��Name$
 Mark��5o      $��8��5i      $��3��5j      $��2��5k      $	��100��5l       � Maximum$ 10Q�5m       � Minimum$ 0Q�5n      $��Right=�5p      6/   5q      5r      