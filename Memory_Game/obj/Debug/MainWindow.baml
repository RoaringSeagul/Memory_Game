   M S B A M L   `   `   `  ����F  BMemory_Game, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Memory_GameMemory_Game  5   	       Memory_Game.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:Memory_Game     5   	   -   5          � Title$  
MainWindow��5   	   $	��600��6   $	��525��6)   .��5	      � ���	#FFBFBFBF.+�5
   
   �� -   5         ��Name$ 
txtPlayer1��5
      $
��Left=�6&   $��20��6A   $��
304,36,0,0q�6M    �� TextWrapping$
 Wrap=�6a   $	��Top=�6u   $��99��6�   
�������6�   �� 5   
   -   5        $ 
txtPlayer2��5      $
��Left=�6&   $��20��6A   $��
408,36,0,0q�6M   $
 Wrap=�6a   $	��Top=�6u   $��99��6�   �� 5   
   -   5         ��Name$ 	lblScore1��5      $��Score Joueur 1��6#   $
��Left=�6<   $��26��6W   $��
304,10,0,0q�6c   $	��Top=�6w   $��99��6�   �� 5   
   -   5        $ 	lblScore2��5      $��Score Joueur 2��6#   $
��Left=�6<   $��26��6W   $��
408,10,0,0q�6c   $	��Top=�6w   $��99��6�    J� RenderTransformOrigin$ 
0.021,0.54&�6�   o� 5   
   -   5         o�Name$
 menu��5      $
��Left=�6   $��20��68   $	��Top=�6D   $��35��6\   .@�5      m� $��_File��6   $��35��6&   $ 0.5,0.5&�61   $��0q�6Q   .@�5      m� -   5        $��New Game��5      6-   �� 5      m� 5      -   5        $��Restart Game��5      61   5      5      o� 5   
   -	   5        $ 	menuDebug��5      $
��Left=�6"   $��20��6=   $	��Top=�6I   $��50��6a   $��36, 0, 0, 0q�6l   .@�5      m� $��_Debug��6   $��50��6'   $ 0.5,0.5&�62   $��0q�6R   .@�5      m� -
   5        $��Show All��5      6-   �� 5      m� 5      -   5        $��Hide All��5      6-   5      5      A� 5   
   -   5         A�Name$ Plateau��5       A� Rows$ 8��6'    A� Columns$ 8��60   $��500px��6<   $��500px��6K   $��Bottom=�6Y   $��Center=�6t   �� 5   
   -   5        $ lblTurn��5      $��                ��6!   $
��Left=�5      $��
110,10,0,0q�6-   $	��Top=�6A   �� 5   
   -   5        	 ��Name$	 btnCPU��5      $��Generate��6!   $
��Left=�64   $��0,25,0,0q�6O   $	��Top=�6a   $��86��6y   6�   5       5!      