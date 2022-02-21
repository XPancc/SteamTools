using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using AndroidX.Annotations;
using AndroidX.Core.Content;
using AndroidApplication = Android.App.Application;

// ReSharper disable once CheckNamespace
namespace System
{
    public static partial class ContextExtensions
    {
        /// <inheritdoc cref="ContextCompat.GetSystemService(Context, Java.Lang.Class)"/>
        public static TService GetSystemService<TService>(this Context context) where TService : class, IJavaObject
        {
            var type = typeof(TService);
            var cls = Java.Lang.Class.FromType(type);
            var service = ContextCompat.GetSystemService(context, cls).JavaCast<TService>();
            if (service == null)
                throw new NullReferenceException(
                    $"ContextCompat.GetSystemService return null, type: {type}");
            return service;
        }

        /// <inheritdoc cref="Context.GetSystemService(string)"/>
        public static TService GetSystemService<TService>(this Context context, string name) where TService : class, IJavaObject
        {
            var service = context.GetSystemService(name)?.JavaCast<TService>();
            if (service == null)
                throw new NullReferenceException(
                    $"Context.GetSystemService return null, name: {name}");
            return service;
        }

        /// <summary>
        /// Return the context of the single, global Application object of the current process.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static Context ApplicationContext(this Context? context) => context?.ApplicationContext ?? AndroidApplication.Context;

        /// <summary>
        /// Returns a color associated with a particular resource ID.
        /// <para>Starting in android.os.Build.VERSION_CODES#M, the returned color will be styled for the specified Context's theme.</para>
        /// <para>https://developer.android.google.cn/reference/androidx/core/content/ContextCompat#getColor(android.content.Context,%20int)</para>
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id">The desired resource identifier, as generated by the aapt tool. This integer encodes the package, type, and resource entry. The value 0 is an invalid identifier.</param>
        /// <returns></returns>
        public static int GetColorCompat(this Context context, [ColorRes] int id)
            => ContextCompat.GetColor(context, id);

        /// <summary>
        /// Returns a drawable object associated with a particular resource ID.
        /// <para>Starting in Build.VERSION_CODES.LOLLIPOP, the returned drawable will be styled for the specified Context's theme.</para>
        /// <para>https://developer.android.google.cn/reference/androidx/core/content/ContextCompat#getDrawable(android.content.Context,%20int)</para>
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id">The desired resource identifier, as generated by the aapt tool. This integer encodes the package, type, and resource entry. The value 0 is an invalid identifier.</param>
        /// <returns></returns>
        public static Drawable GetDrawableCompat(this Context context, [DrawableRes] int id)
            => ContextCompat.GetDrawable(context, id);

        /// <inheritdoc cref="Android.Content.Res.Resources.GetDimensionPixelSize(int)"/>
        public static int GetDimensionPixelSize(this Context context, [DimenRes] int resId)
        {
            var value = context.Resources!.GetDimensionPixelSize(resId);
            return value;
        }
    }
}