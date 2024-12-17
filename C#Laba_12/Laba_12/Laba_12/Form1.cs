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
        String[] arraySortNames = new String[] { "Обмен", "Выбор", "Включение", "Шелла", "Быстрая", "Линейная", "Встроенная" };
        Boolean[] arraySortChecked = new bool[] { false, false, false, false, false, false, false };
        const int ROWS_COUNT = 7;
        int arraySize;
        int[] array;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.RowCount = ROWS_COUNT;
            for (int i = 0; i < ROWS_COUNT; i++)
            {
                dataGridView1.Rows[i].Cells[1].Value = arraySortNames[i];
                dataGridView1.Rows[i].Cells[0].Value = arraySortChecked[i];
            }
        }
        private void buttonSort_Click(object sender, EventArgs e)
        {
            // Выделение колонок для заполнения 
            int timeCell = 4;
            int permutationsCell = 3;
            int comparisonsCell = 2;
            // Создание и заполнение массива 
            Random rnd = new Random();
            arraySize = (int)numericUpDown_arraySize.Value;
            array = new int[arraySize];
            for (int i = 0; i < array.Length; i++) array[i] = rnd.Next(1, 10);

            // Заполнение массива checked
            for (int i = 0; i < ROWS_COUNT; i++)
            {
                arraySortChecked[i] = (bool)dataGridView1.Rows[i].Cells[0].Value;
            }

            // Проверка состояния checked и вызов определённой функции
            if (arraySortChecked[0])
            {
                InfoSort sortBubbleSort = BubbleSort(array);
                dataGridView1.Rows[0].Cells[timeCell].Value = sortBubbleSort.Time;
                dataGridView1.Rows[0].Cells[permutationsCell].Value = sortBubbleSort.Permutations;
                dataGridView1.Rows[0].Cells[comparisonsCell].Value = sortBubbleSort.Comparisons;
            }
            if (arraySortChecked[1])
            {
                InfoSort sortDirectSelection = SortDirectSelection(array);
                dataGridView1.Rows[1].Cells[timeCell].Value = sortDirectSelection.Time;
                dataGridView1.Rows[1].Cells[permutationsCell].Value = sortDirectSelection.Permutations;
                dataGridView1.Rows[1].Cells[comparisonsCell].Value = sortDirectSelection.Comparisons;
            }
            
            
            
            
        }
        public InfoSort BubbleSort(int[] array)
        {
            int comparisons = 0;
            int permutations = 0;

            // Создаётся копия массива, дабы не сортировать исходный.
            int[] arrayCopy = new int[array.Length];
            Array.Copy(array, arrayCopy, array.Length);
            int StartTime = Environment.TickCount;
            {
                bool swapped;
                for (int i = 0; i < arrayCopy.Length - 1; i++)
                {
                    swapped = false;
                    for (int j = 0; j < arrayCopy.Length - i - 1; j++)
                    {
                        if (arrayCopy[j] > arrayCopy[j + 1])
                        {
                            comparisons++;
                            int temp = arrayCopy[j];
                            arrayCopy[j] = arrayCopy[j + 1];
                            arrayCopy[j + 1] = temp;
                            swapped = true;
                            permutations++;
                        }
                        comparisons++;
                    }
                    // Если на каком-то проходе не было обменов, массив отсортирован
                    if (!swapped)
                        break;
                }
                int ResultTime = Environment.TickCount - StartTime;
                return new InfoSort { Time = ResultTime, Comparisons = comparisons, Permutations = permutations };
            }
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
                        comparisons++;
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