   M S B A M L   `   `   `  ���� M  IWPF_Complex_Calcul, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:WPF_Complex_CalculWPF_Complex_Calcul  #    WPF_Complex_Calcul.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     2local clr-namespace:WPF_Complex_Calcul     X TXceed.Wpf.Toolkit, Version=4.6.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  2
syncfusion!http://schemas.syncfusion.com/wpf  -     �Name$  MainWindow1�� � Title$ Complex number calculator��$	��282��$	��640��.��� -    �Name$ MainGrid�����	#FFCCFEFF.+��� -    ��Name$ label_z1��$��z1 = ��$
��Left=�$��
151,19,0,0q�$	��Top=�$��14(�$��29��$��41���� -   $ label_z2��$��z2 = ��$
��Left=�$��
151,60,0,0q�$	��Top=� J� RenderTransformOrigin$ 0.573,0.502&�$��14(�$��29��$��41���� $��+��$
��Left=�$��
318,19,0,0q�$	��Top=�$��14(�$��29��$��20���� $��+��$
��Left=�$��
318,60,0,0q�$	��Top=�$ 0.368,-0.114&�$��14(�$��29��$��20���� $��i��$
��Left=�$��
463,22,0,0q�$	��Top=�$��14(�$��29��$��13���� $��i��$
��Left=�$��
463,63,0,0q�$	��Top=�$ 0.491,0.541&�$��14(�$��29��$��13���� -   $ LabelSum��$��	z1 + z2= ��$
��Left=�$��
24,110,0,0q�$	��Top=�$��14(�$��29��$��68���� -   $ LabelSub��$��
z1 - z2 = ��$
��Left=�$��
24,136,0,0q�$	��Top=�$��14(�$��29��$��68���� -   $ 	LabelMult��$��
z1 * z2 = ��$
��Left=�$��
24,161,0,0q�$	��Top=�$��14(�$��29��$��68���� -   $ LabelDiv��$��
z1 / z2 = ��$
��Left=�$��
24,187,0,0q�$	��Top=�$��14(�$��29��$��68���� -	    ��Name$ 
Sum_holder��$
��Left=�$��
92,113,0,0q� �� TextWrapping$
 Wrap=�$��
Ответ��$	��Top=�$	��196�� �� IsReadOnlyCaretVisible . $��16(�$��23��{�.  �� -
   $ 
Sub_holder��$
��Left=�$��
92,139,0,0q�$
 Wrap=�$��
Ответ��$	��Top=�$	��196�� . $��16(�$��23��{�.  �� -   $ Mult_holder��$
��Left=�$��
92,164,0,0q�$
 Wrap=�$��
Ответ��$	��Top=�$	��196�� . $��16(�$��23��{�.  �� -   $ 
Div_holder��$
��Left=�$��
92,190,0,0q�$
 Wrap=�$��
Ответ��$	��Top=�$	��196�� . $��16(�$��23��{�.  �� $��Аргумент z1 =��$
��Left=�$��293,135,0,0q�$	��Top=�$��14(�$��29��$	��101���� $��Модуль z1 =��$
��Left=�$��293,161,0,0q�$	��Top=�$��14(�$��29��$��91���� -   $ 
Arg_holder��$
��Left=�$��401,138,0,0q�$
 Wrap=�$��
Ответ��$	��Top=�$	��196�� . $��16(�$��23��{�.  �� -   $ 
Mod_holder��$
��Left=�$��401,164,0,0q�$
 Wrap=�$��
Ответ��$	��Top=�$	��196�� . $��16(�$��23��{�.  %  Xceed.Wpf.Toolkit.DecimalUpDown  -     Name$ z1Real_input��,  &Xceed.Wpf.Toolkit.Primitives.InputBase	   AllowTextInput	 .  �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Decimal, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]
   	AllowSpin
 . �  �Xceed.Wpf.Toolkit.NumericUpDown`1[[System.Nullable`1[[System.Decimal, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   FormatString$ F4��   Text$ 1.000��   	Increment$ 1X�$��26��$	��120��$	��Top=�$
��Left=�$��
192,22,0,0q�   MouseWheelActiveTrigger$ 	MouseOver=�  -   $ z1Imag_input��	 .  
 . $ F4��$ 1.000��$ 1X�$��26��$	��120��$	��Top=�$
��Left=�$��
338,22,0,0q�$ 	MouseOver=�  -   $ z2Real_input��	 .  
 . $ F4��$ 2.000��$ 1X�$��26��$	��120��$	��Top=�$
��Left=�$��
192,61,0,0q�$ 	MouseOver=�  -   $ z2Imag_input��	 .  
 . $ F4��$ 4.000��$ 1X�$��26��$	��120��$	��Top=�$
��Left=�$��
338,61,0,0q�$ 	MouseOver=�