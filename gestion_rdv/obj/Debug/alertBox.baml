   M S B A M L   `   `   `  ����  gestion_rdv    gestion_rdv.alertBox   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   V RMaterialDesignThemes.Wpf, Version=3.1.0.1244, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  alertBox��5   	    � WindowStyle$
 None=�6   $	��244��6-   $��80��69   $	��410��6E    � Topmost .  � SizeToContent$ WidthAndHeight=�5      .��5      �  � RowDefinitions 5   
   �� 5	      $
��auto �6   �� 5
      $
��auto �6   �� 5      $
��auto �6   .+�5   
   � $��0��6   
������$	��Top=�64   .+�5      �� 
������6   �� 5      
������6   �� 5      
������6   5      �� 5   
   -   5        $��0��5      
������65   �� 5   
   $��1��6   $��Center=�6"   $��5q�6?   .+�5      �� -   5        $��messageLabel��5   ,   $
��Wrap=�6   $��14(�6@   �� 5      5      �� 5   
   -   5        $��type0��5   `   $��  OK  ��6   $��2��6"   $��Center=�6/   $y�Hidden=�6L   $��5q�5      �� 5   
   -   5        $��type1��5   X    �� Orientation$ 
Horizontal=�6   $��2��6.   $��Center=�6;   $y�Hidden=�6e   $��5q�6y   .+�5      �� -   5        $��	yesButton��5      $��	CONFIRMER��6&   
������6R   �� 5       �� 5!      -   5        $��noButton��5!      $��ANNULER��6%   
���  ��6H   5"      5#      5$      