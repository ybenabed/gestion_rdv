   M S B A M L   `   `   `  ����  gestion_rdv    gestion_rdv.showRdv   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   V RMaterialDesignThemes.Wpf, Version=3.1.0.1244, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  showRdv��5   	   $	��450��6   $	��350��6&   $	��350��62   $	��350��6A    � WindowStyle$
 None=�6P   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5	      $
��Auto �6   �� 5
      $��* �6   .+�5   
   � -   5        $��ggrid��5   d   ���#0c4a60$��0��6$   $	��Top=�61   .+�5      �� .+�5      ��  �� Orientation$ 
Horizontal=�6   $��Right=�66   .+�5      �� -   5        $��closeButton��5      
������ $��Center=�6I   $
��auto��6d   $
��auto��6r   .��5      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Close=�62   $��15��6?   $��15��6K   5      �� 5      5      5      5      �� 5   
   -   5        $
��date��5      ���	#FFD54A42$��16(�63   $
��Bold"�6A   $��1��6S   $	��Top=�6`   �� 5   
   -   5        $��	appscroll��5      $��2��6(   .��5      �� -   5        $��appointementStack��5      .+�5      � -   5        $��5q�5      $y�	Collapsed=�6f   .+�5      �� 
���   � �� RadiusX$ 10��6/    �� RadiusY$ 10��6<   5      �� 5       .+�5!      �� $ 
Horizontal=�6%   .+�5"      �� -   5        $��	nameLabel��5"   m   $��Benabed Youcef à ��6$   $
��Bold"�6@   $��Center=�6R   6m   �� 5#      .��5$   "   �� $ 
Horizontal=�6-   .+�5%   &   �� -   5        $
��hour��5&   l   
������ .  (MaterialDesignThemes.Wpf.TextFieldAssist   DecorationVisibility$ Hidden=�5%   H   $��12(�5&   2    �� 	MaxLength$ 2��6@   $��0q�6N   $��00��6b   5(   .   �� 5)   &   $��:��6,   �� 5*   &   -	   5        $��minute��5+   l   
������ $ Hidden=�5*   H   $��12(�5+   2   $ 2��6@   $��0q�6N   $��00��6b   5-   .   5.   #   5/      50      �� 51      -
   5        $��description��52   :   $��Stretch=�51   "   $��10q�6@   	 �� AcceptsReturn	 . 
 �� TextWrapping$

 Wrap=�52   &   6:   �� 53      $ 
Horizontal=�6%   $��Right=�6>   .+�54      �� -   5        $��
ajouterRDV��54   2    � Cursor$
 Hand��6$   .��55   "   �� $��	Underline{�6,   $��Ajouter��6H   
���� ��6W   56      �� 57      -   5        $��annulerAjout��57   2   $
 Hand��6$   .��58   "   �� $��	Underline{�6,   $��Annuler��6H   
���� ��6W   59      5:      5;      5<      5=      5>      �� 5@   
   -   5        $��addNewLabel��5@   O   $
 Hand��6   $��Center=�6   $y�Hidden=�6;   .��5A        $
 Plus=�6&   ���	#FF84F184$��40��6I   $��40��6T   5B      5D      5E      