using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Prolificinteractive.Materialcalendarview;
using Com.Prolificinteractive.Materialcalendarview.Format;
using Java.Util;

namespace Demo
{
    public class CustomDatePicker : Dialog
    {
        MaterialCalendarView calendarView;
        ImageView btnCancel;
        ImageView btnNext;
        ImageView btnPrevious;
        TextView txtHeader;

        public CustomDatePicker(Context context) : base(context)
        {
            Init();
        }

        public CustomDatePicker(Context context, int themeResId) : base(context, themeResId)
        {
            Init();
        }

        protected CustomDatePicker(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Init();
        }

        protected CustomDatePicker(Context context, bool cancelable, EventHandler cancelHandler) : base(context, cancelable, cancelHandler)
        {
            Init();
        }

        protected CustomDatePicker(Context context, bool cancelable, IDialogInterfaceOnCancelListener cancelListener) : base(context, cancelable, cancelListener)
        {
            Init();
        }

        async void Init()
        {
            RequestWindowFeature((int)WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.custom_date_picker);
            SetCancelable(true);
            Window.SetLayout(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
            Window.SetBackgroundDrawable(new ColorDrawable(Android.Graphics.Color.Transparent));

            calendarView = FindViewById<MaterialCalendarView>(Resource.Id.calendar_view);
            ViewGroup group1 = (ViewGroup)calendarView.GetChildAt(0);
            ViewGroup group2 = (ViewGroup)calendarView.GetChildAt(1);
            View header = calendarView.GetChildAt(0);
            header.Visibility = ViewStates.Gone;
            calendarView.SetCurrentDate(Calendar.Instance);
            calendarView.AddDecorators(new CurrentDayDecorator(Context));
            calendarView.SetWeekDayFormatter(new ArrayWeekDayFormatter(new string[] { "M", "T", "W", "T", "F", "S", "S" }));
            calendarView.InvokeState().Edit().SetFirstDayOfWeek((int)Java.Util.Calendar.Saturday);


            btnCancel = FindViewById<ImageView>(Resource.Id.btnCancel);
            btnNext = FindViewById<ImageView>(Resource.Id.btnNext);
            btnPrevious = FindViewById<ImageView>(Resource.Id.btnPrevious);
            txtHeader = FindViewById<TextView>(Resource.Id.tvHeader);

            btnCancel.Click += BtnCancel_Click;
            btnNext.Click += BtnNext_Click;
            btnPrevious.Click += BtnPrevious_Click;
            calendarView.SetOnMonthChangedListener(new OnMonthChangedListener
            {
                MonthChanged = (calendarView, day) =>
                {
                    DateTime date = new DateTime(day.Year, day.Month + 1, day.Day);
                    txtHeader.Text = "" + date.ToString("MMMM yyyy");
                }
            });
        }

        private void BtnPrevious_Click(object sender, EventArgs e)
        {
            calendarView.GoToPrevious();
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            calendarView.GoToNext();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Dismiss();
        }
    }

    class CurrentDayDecorator : Java.Lang.Object, IDayViewDecorator
    {
        private readonly Context context;
        public CurrentDayDecorator(Context context)
        {
            this.context = context;
        }
        CalendarDay toDay = CalendarDay.Today();

        public void Decorate(DayViewFacade p0)
        {
            p0.SetBackgroundDrawable(context.Resources.GetDrawable(Resource.Drawable.bg_current_date));
        }

        public bool ShouldDecorate(CalendarDay p0)
        {
            return toDay.Day == p0.Day && toDay.Month == p0.Month && toDay.Year == p0.Year;
        }
    }

    class OnMonthChangedListener : Java.Lang.Object, IOnMonthChangedListener
    {
        public Action<MaterialCalendarView, CalendarDay> MonthChanged { get; set; }
        public void OnMonthChanged(MaterialCalendarView p0, CalendarDay p1)
        {
            MonthChanged?.Invoke(p0, p1);
        }
    }
}