using System;
using Android.Content;
using Android.Runtime;
using Android.Util;
using FFImageLoading.Cross;
#nullable enable

namespace ArmaOps.Droid.Common.Views
{
    [Register("armaops.droid.common.views.CachedImageView")]
    public class CachedImageView : MvxCachedImageView
    {
        public CachedImageView(Context context) : base(context)
        {
            ErrorPlaceholderImagePath = "res:error_placeholder";
            LoadingPlaceholderImagePath = "res:error_placeholder";
        }

        public CachedImageView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            ErrorPlaceholderImagePath = "res:error_placeholder";
            LoadingPlaceholderImagePath = "res:error_placeholder";
        }

        public CachedImageView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            ErrorPlaceholderImagePath = "res:error_placeholder";
            LoadingPlaceholderImagePath = "res:error_placeholder";
        }
    }
}
