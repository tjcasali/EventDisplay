   M S B A M L   `   `   `  ����G  CEventDisplay, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:EventDisplayEventDisplay  5   	   "    EventDisplay.AllEventsWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:EventDisplay     5   	   -   5          �Name$  AllEventsWindow1��5   	    � Title$ LPL: All Events��5   	   $
��1080��6!   $
��1920��6/    � WindowState$ 	Maximized=�6<    � WindowStyle$
 None=�6T   ��5      �� 5   
    �� ImageSource$" Images/DailyEventDisplay.jpg��6   .��5      �  � ColumnDefinitions 5   
   �� 5      $��9* �6   �� 5      $
��Auto �6   $
��1836��6,   �� 5      $	��10* �6   .+�5   
   �� -   5         ��Name$ AEWTodaysEventsTB��5      $��Center=�6-   $��75��6J   $��665,56,663,0q�6V   $��Today's Events��6l   $	��Top=�5      $	��508��6*   $��60(�66   $��Century Gothic)�6D   $
��Bold"�6`   ���	#FF1F5D46 �� TextAlignment$ Center=�5       �� 
IsReadOnly . $��1��6;   ��5      Z� 	 �� SelectionBrush	 6@   Z� 5   ;   �� 5   
   -   5        $ 	AEWDateTB��5      $��Center=�6    $��78��6=   
 �� TextWrapping$

 Wrap=�5      $	��Top=�6!   $	��436��69   $��38(�6E   $ Center=�6S   ���	#FF1F5D46$��1��5   $    . $
��Bold"�5       $��Century Gothic)�6   $��711,155,689,0q�6;   ��5   4   Z� ��6J   Z� .��5    R    J� Effect 5!      3  -System.Windows.Media.Effects.DropShadowEffect  5"         
BlurRadius$ 5Q�6#      ShadowDepth$ 2Q�62      Color$ White��6B   6R   .��5#      5$      �� 5&   
   -   5         ��Name$ AEWCloseWindow��5&      $��Quit to Main Menu��6)   $
��Left=�6E   $��1691,876,0,0q�6`   $	��Top=�5'      $	��125��6)   $��60��65   $��1��5(       J� RenderTransformOrigin$ 0.988,1.217&�6>   ��5'   A   Z� ��6X   Z� ��6n   Z� 5(   >   �� 5*   
   -   5         ��Name$ AEWListView��5*      $
��Left=�6(   $	��765��6C   $��206,265,0,0q�6P   $	��Top=�6e   $
��1450��6}   
���   �$��Arial)�5+   S   $��24(�6f   $��1��6t   ��6   Z� ��6*   Z�  � 	Resources 5,       �� 
TargetType �� Property �� Value��%Y   '��       �� 5-      " ��6   .�5.      ��  � y�$ 	Collapsed=�63   5/      50       52        53      $ 5Q�6#   $ 3Q�62   $ White��6B   6R   ��56      �� 57      " ��6    �� Triggers 58      �� 59       �� Binding 6&   ��  �� Path$
 Name� �� Value Children's Programs6D   .a�5:      ��  � ��$
 Bold"�6;   �� 5;       � ��$ 30 px(�69   �� 5<       � �� �	#FF1255466;   �� 5=       � ��$ Century Gothic)�6;   5>      �� 5?       6&   �� $
 Name� Teen Programs6D   .a�5@      ��  � ��$
 Bold"�6;   �� 5A       � ��$ 30 px(�69   �� 5B       � �� �	#FF1255466;   �� 5C       � ��$ Century Gothic)�6;   5D      �� 5E       6&   �� $
 Name� Adult Programs6D   .a�5F      ��  � ��$
 Bold"�6;   �� 5G       � ��$ 30 px(�69   �� 5H       � �� �	#FF1255466;   �� 5I       � ��$ Century Gothic)�6;   5J      5L       �� View 5O      �� 5P      .P�5Q      �� $
��Time��6%    �� Width$	 300��6Y    �� DisplayMemberBinding 63   �� *Time+6Y   �� 5R      $
��Name��6%   $	 800��6Y    63   �� *Name+6Y   �� 5S      $
��Room��6%   $	 300��6Y    63   �� *Room+6Y   5T      5V      5W      5X      