   M S B A M L   `   `   `  ����F  BMinesweeper, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:MinesweeperMinesweeper  5   	   X TXceed.Wpf.Toolkit, Version=3.6.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4P:clr-namespace:Xceed.Wpf.Toolkit;assembly=Xceed.Wpf.ToolkitXceed.Wpf.Toolkit 5   	       Minesweeper.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   4xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   %localclr-namespace:Minesweeper  5   	   HwpfTool:clr-namespace:Xceed.Wpf.Toolkit;assembly=Xceed.Wpf.Toolkit  5   	     � Title$  
MainWindow��5   	    � 	Resources 5	      9  3Xceed.Wpf.Toolkit.Core.Converters.AdditionConverter   AdditionConverter  ButtonFocusVisual �� Property �� Value J� SnapsToDevicePixels �� StrokeDashArray  ButtonNormalBackground �� EndPoint �� 
StartPoint � Color	 � Offset  ButtonNormalBorder
 �� Color  ToggleButtonStyle1 �� 
TargetType �� 
TargetType �� Orientation ��Name �� CornerRadius ��Name �� Triggers P� Property P� Value �� 
TargetName��%�  &	        &	      &	 �     &	 �    &	 �    7  7  7    5
   
   6!   �� 5   
   .�5      ��  � �� 5      �� 5      .R�5      �� $��2q�6$    . #��� ��5   $   $��1��5   $   $	 1 2R�5   $   5      5      5      �� 5   
   $	 0,1&�5      $	 0,0&�5      .��5      � $ #F3F3F3��6   $	 0Q�5      � 5      $ #EBEBEB��6   $		 0.5Q�5      � 5      $ #DDDDDD��6   $		 0.5Q�5       � 5!      $ #CDCDCD��6   $	 1Q�5"      5#      �� 5$   
   $
 	#FF707070��5%      �� 5&   
   " d�5'      .�5(      ��  �@�FocusVisualStyle8   5)      �� 5*       � ��8  5+      �� 5,       � ��8  5-      �� 5.       � ��$ 1q�5/      �� 50       � ��# � ��51      �� 52       � ��$ Center=�53      �� 54       � ��$ Center=�55      �� 56       � ��$ 1q�57      �� 58       � �� 59      �� 5:      " d�6&   .R�5;      �� $ 
Horizontal=�6%   .+�5<      �� #��z��6/   #��z��5=   /   ��. # z 5?   /   #��z��5@   /   �� 5A      $ on��6%   $��25��5B   %   $��25��5C   %   
�������$ 2,0,0,4��5E   %   $��10,0,0,0q�5F   %   ._�5G   "   �� $ onText��6,   $��On��5H   ,   $��Center=�5I   ,   $��Center=�5J   ,   5K      �� 5L      $	 off��6%   $��25��5M   %   $��25��5N   %   
�������$ 0,2,4,0��5P   %   ._�5Q   "   �� $ offText��6,   $	��Off��5R   ,   $��Center=�5S   ,   $��Center=�5T   ,   5U      5V       5W      P� 5X       � {� .  . �5Z   "   ��  � �� �#ADADAD5[   )   5\      P� 5]       �@d�	IsChecked$
 trueY�5^   &   . �5_   "   �� $ on��6)    � ��
 ��ح�5a   )   �� 5b   "   $ onText��6)    � ��
 �����5d   )   5e      P� 5f       �@d�	IsChecked$ FalseY�5g   &   . �5h   "   �� $	 off��6)    � ��
 ��ح�5j   )   �� 5k   "   $ offText��6)    � ��
 �����5m   )   5n      5p      5r      5s      5t      .��5u      � .+�5v   
   �� $	��Top=�6   $ 
Horizontal=�5w      $
��Left=�5x      $��0,5,0,0q�5y      .+�5z      %  Xceed.Wpf.Toolkit.IntegerUpDown  -   5          Name$ label��5z   $   �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Value$	 100X�5{   $   $
��Bold"�5|   $   $��16(�5}   $   
���   ����	#FF57FF00$��Center=�5�   $   $��Center=�5�   $   $��Center=�5�   $   $��75��5�   $   {�.  $��Center=�5�   $      ShowButtonSpinner .  5�   $   �� 5�      -   5         ��Name$ button��5�      $��Reset��5�       �� Left$ 63��5�      
 �� Top$ 17��5�      $��75��5�      $��20,0,0,0q�5�      5�      d� 5�      -   5         d�Name$ 
FlagToggle��5�      $
��Flag��5�      #��[ 5�      $��25,0,0,0q�5�      �� 5�      -   5        $
��Auto��5�      $ 63��5�      $ 17��5�      $��75��5�      $
��20,0q�5�      5�      5�      �� 5�   
   $��Right=�6   $	��Top=�5�      $ 
Horizontal=�5�      $��	0,10,10,0q�5�      .+�5�      �� $��Number of mines��6   $��Center=�5�        5�      -   5        $ 
NumOfMines��5�   $   $	 200X�5�   $   $��Center=�5�   $   $��5,0,25,0q�5�   $      Minimum$ 1X�5�   $   5�   $   �� 5�      $��Width��6   $��Center=�5�        5�      -   5        $ width��5�   $   $ 40X�5�   $   $��Center=�5�   $   $��5,0,25,0q�5�   $   $ 1X�5�   $   5�   $   �� 5�      $��Hight��6   $��Center=�5�        5�      -   5        $ hight��5�   $   $ 20X�5�   $   $��Center=�5�   $   $��5,0,0,0q�5�   $   $ 1X�5�   $   5�   $   5�      �� 5�   
   $��0,45,0,0q�6   $
��Auto=�5�      $
��Auto=�5�      ���	#FFDADADA.��5�      ]� ��. $��Center=�5�      $��Center=�5�      .+�5�      �� -   5         ��Name$ board��5�      $��20q�5�      $ 
Horizontal=�5�      5�      5�      5�      5�      