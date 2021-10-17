﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CourseOOP.Models;
using Newtonsoft.Json;

namespace CourseOOP.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Triangle tr = Triangle.Parse("(,) (1,0) (0,1)");
            //Hexagon hexagon = Hexagon.Parse("4");
            _ = new ShapeHandler(@"D:\Workspace\CourseOOP\CourseOOP\Data\figures.txt");
        }
    }
}
