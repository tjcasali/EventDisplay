   M S B A M L   `   `   `  ����G  CEventDisplay, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:EventDisplayEventDisplay  5   	       EventDisplay.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:EventDisplay     5   	   -   5          � Title$  LPL:Event Display��5   	   $	��710��6#   $	��851��60   ��5
      �� 5   
    �� ImageSource$ Images/LPLCoverPhoto.png��6   .��5      �� -   5         ��Name$ MWCanvas��5      .+�5   
   �  �� Left$ 10��6   
 �� Top$ 10��6    $	��659��60   $	��823��6=   .+�5      �� -   5         ��Name$ AllEventsButton��5      $��Display All Events��6.   $
��Left=�6K   $��287,282,0,0q�6f   $	��Top=�6{   $	��244��6�   $��73��6�   ���	#FF125546
�������$��Corbel)�6�   $��18(�6   J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         
BlurRadius$ 5Q�6'      ShadowDepth$ 2Q�66   	   Color$	 White��6F   6V   5      �� 5      -   5        $ CommunityRoomEvents��5      $��Community Room��62   $
��Left=�6K   $��
72,446,0,0q�6f   $	��Top=�6z   $	��170��6�   $��52��6�   ���	#FF125546
�������$��Corbel)�6�   $��18(�6
   5        5      $ 5Q�6'   $ 2Q�66   $	 White��6F   6V   5      �� 5      -   5        $ ClemensRoomEvents��5      $��Clemens Room��60   $
��Left=�6G   $��569,446,0,0q�6b   $	��Top=�6w   $	��170��6�   $��52��6�   ���	#FF125546
�������$��Corbel)�6�   $��18(�6   5        5      $ 5Q�6'   $ 2Q�66   $	 White��6F   6V   5      �� 5      -   5        $ KovarikRoomEvents��5      $��Kovarik Room��60   $
��Left=�6G   $��321,446,0,0q�6b   $	��Top=�6w   $	��169��6�   $��52��6�   ���	#FF125546
�������$��Corbel)�6�   $��18(�6   5         5!      $ 5Q�6'   $ 2Q�66   $	 White��6F   6V   5#      �� 5$      $
��Left=�6   $��84��61   $��
208,87,0,0q�6=   $��LPL: Event Display��6Q   $	��Top=�6k   $	��404��6�   $��48(�6�   ���	#FF084F2C$��Segoe WP Light)�6�   
 �� 
IsReadOnly
 . ��6�   Z� ��6�   Z� .��6   5%        5&      $ 5Q�6'   $ 2Q�66   $	 White��6F   6V   .��5'   "   5(      5*      5+      5,      