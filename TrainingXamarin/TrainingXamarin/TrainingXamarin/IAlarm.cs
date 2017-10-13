using System;
using TrainingXamarin.Model;

namespace TrainingXamarin
{
    public interface IAlarm
    {
        void SetAlarm(Todo work);
        void CancelAlarm(Todo work);
    }
}
