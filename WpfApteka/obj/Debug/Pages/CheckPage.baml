   M S B A M L   `   `   `  ����D  @WpfApteka, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WpfApteka.PagesWpfApteka.Pages  5          WpfApteka.Pages.CheckPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:WpfApteka.Pages     5      -   5          N� Title$  	CheckPage��5	      .��5
      �  � RowDefinitions 5   
   �� 5      $��35 �6   �� 5      $��35 �6   �� 5      $	��315 �6   �� 5      $	��120 �6    � ColumnDefinitions 5   
   �� 5      -   5         ��Name$ CheckSplitterColumn��5   :   $	��462 �6   $	��462��6+   6:   �� 5      .+�5   
   �� ���#cee0f2$��0��6#   $��0��60   $��5��6@   $��2��6Q   �� 5   
   $��Чек��6   $��3��6   $��16(�62   $��Bottom=�6@   $��
ExtraBlack"�6[   �� 5   
   ���#cee0f2 �� Orientation$ 
Horizontal=�6*   $��1��6C   $��3��6P   .+�5      �� $��Фильтрация��6   $��16(�6)   $��Center=�67   $
��Bold"�6R   �� 5      -   5        $��FilterCheckComboBox��5      $	��150��62   $	��5,5q�6>   6K   �� 5      -   5         ��Name$ FilterCheckTextBox��5      $	��200��62   $	��0,5q�6>   6K   5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ CheckDataGrid��5      $��2��6*   $��2��67      AutoGenerateColumns .     CanUserAddRows .  	   Columns	 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn
   Header$
 Сотрудник��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6]   1  +System.Windows.Controls.DataGridBoundColumn   Binding 68   ��  �� Path$ WORKERS.FIO�6]     5      $
 Время продажи��6%   $ * 6_    6<   �� $ 	TIME_SALE�6_   4  .System.Windows.Controls.DataGridTemplateColumn  5       $ * 6)   $
 Дата продажи��5#   5      CellTemplate 5$      �� 5%      .R�5&      �� ��6$   �� $ 	DATE_SALE�5'      5)      5+      �� 57   
   -   5         ��Name$ listBox1��57      $��3��6$   $��0��61   $��
5,10,200,0q�6A   ��58      �� 59      .R�5:      �� .+�5;      �� ��6$   �� $ WORKERS.FIO��� 5<      ��6$   �� $ 	TIME_SALE��� 5=      ��6$   �� $ 	DATE_SALE� �� StringFormat$ 
dd-MM-yyyy��5>      5?      5A      �� 5B   
   -   5        $ ��Добавить в чек��5B      $��3��6*   $��2��67   $	��120��6G   $��25��6S   $��Bottom=�6_   $��Right=�6z   $��0 0 5 35q�6�   6�   �� 5C   
   -   5        $��Оформить чек��5C      $��3��6(   $��2��65   $	��120��6E   $��25��6Q   $��Bottom=�6]   $��Right=�6x   $��0 0 5 0q�6�   6�   5D      5E      