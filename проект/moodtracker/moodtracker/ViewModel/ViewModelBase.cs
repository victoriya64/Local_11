using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace moodtracker.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        //событие, которое происходит при изменении свойств ViewModel.
        public event PropertyChangedEventHandler PropertyChanged;
        //метод для вызова события PropertyChanged при изменении свойств ViewModel.
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //устанавливает значение поля и вызывает OnPropertyChanged, если значение было изменено.
        //Сравнивает старое и новое значение поля, и если они не равны, устанавливает новое значение и вызывает
        //OnPropertyChanged для уведомления View об изменении свойства. Это позволяет избежать избыточного
        //обновления интерфейса в случае, когда значение свойства осталось прежним.
        public void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                OnPropertyChanged(propertyName);
            }
        }
    }
}
