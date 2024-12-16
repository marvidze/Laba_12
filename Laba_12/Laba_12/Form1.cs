using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laba_12
{
    public partial class Form1 : Form
    {
        String[] arraySortNames = { "Обмен", "Выбор", "Включение", "Шелла", "Быстрая", "Линейная", "Встроенная" };
        int arraySize;
        int[] array;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.RowCount = 7;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Cells[1].Value = arraySortNames[i];
            }
        }
        private void buttonSort_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            arraySize = (int)numericUpDown_arraySize.Value;
            array = new int[arraySize];
            int time = 4;
            int permutations = 3;
            int comparisons = 2;
            for (int i = 0; i < array.Length; i++) array[i] = rnd.Next(1, 10);
            InfoSort sortDirectSelection = SortDirectSelection(array);
            dataGridView1.Rows[1].Cells[time].Value = sortDirectSelection.Time;
            dataGridView1.Rows[1].Cells[permutations].Value = sortDirectSelection.Permutations;
            dataGridView1.Rows[1].Cells[comparisons].Value = sortDirectSelection.Comparisons;
        }

        public InfoSort SortDirectSelection(int[] _arr)
        {
            int comparisons = 0;
            int permutations = 0;

            int[] arr = new int[_arr.Length]; // Создаётся копия массива, дабы не сортировать исходный.
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = _arr[i];
            }

            int StartTime = Environment.TickCount;
            {
                for (int i = 0; i < arr.Length - 1; i++)
                {
                    int minIndex = i; // Индекс минимального элемента в неотсортированной части массива.

                    for (int j = i + 1; j < arr.Length; j++)  // Поиск минимального элемента в неотсортированной части массива
                    {
                        if (arr[j] < arr[minIndex])
                        {
                            minIndex = j;
                            comparisons++;
                        }
                    }
                    
                    // Swap.
                    int temp = arr[i];
                    arr[i] = arr[minIndex];
                    arr[minIndex] = temp;
                    permutations++; 
                }
            }
            int ResultTime = Environment.TickCount - StartTime;
            return new InfoSort { Time = ResultTime, Comparisons = comparisons,  Permutations = permutations};
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Close();
        }
    }
}