using FFXIVRelicTracker.Helpers;
using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows;

namespace FFXIVRelicTracker.ARR.Animus
{
    
    public class AnimusObject :ObservableObject
    {
        public string name{ get; set;}
        public string person{ get; set;}
        public string type{ get; set;}
        public string map{ get; set;}
        public BoolObject completed{ get; set;}
        public PointF pointF{ get; set;}

        public AnimusObject()
        {
            completed = new BoolObject();
        }
        public AnimusObject(bool boolean)
        {
            completed = new BoolObject();
            this.Completed.Bool = boolean;
        }
        public AnimusObject(AnimusObject animusObject)
        {
            completed = new BoolObject();
            this.Name = animusObject.Name;
            this.Completed = animusObject.Completed;
            this.PointF = animusObject.PointF;
            this.Map = animusObject.Map;
            this.Person = animusObject.Person;
            this.Type = animusObject.Type;
        }
        public string Map
        {
            get { return map; }
            set
            {
                map = value;
                OnPropertyChanged(nameof(Map));
            }
        }
        public double X
        {
            get { return (800 * PointF.X / 42) - 15; }
        }
        public double Y
        {
            get { return (790 * PointF.Y / 42) - 15; }
        }
        public string Person
        {
            get { return person; }
            set
            {
                person = value;
                OnPropertyChanged(nameof(Person));
            }
        }
        public string Type
        {
            get { return type; }
            set
            {
                type = value;
                OnPropertyChanged(nameof(Type));
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public BoolObject Completed
        {
            get { return completed; }
            set
            {
                completed = value;
                OnPropertyChanged(nameof(Completed));
            }
        }

        public PointF PointF
        {
            get { return pointF; }
            set
            {
                pointF = value;
                OnPropertyChanged(nameof(PointF));
            }
        }
    }
}
