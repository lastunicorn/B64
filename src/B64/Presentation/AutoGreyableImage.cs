// B64
// Copyright (C) 2016 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DustInTheWind.B64.Presentation
{
    public class AutoGreyableImage : Image
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoGreyableImage"/> class.
        /// </summary>
        static AutoGreyableImage()
        {
            // Override the metadata of the IsEnabled property.
            IsEnabledProperty.OverrideMetadata(typeof(AutoGreyableImage), new FrameworkPropertyMetadata(true, OnAutoGreyScaleImageIsEnabledPropertyChanged));
        }

        /// <summary>
        /// Called when [auto grey scale image is enabled property changed].
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnAutoGreyScaleImageIsEnabledPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs args)
        {
            AutoGreyableImage autoGreyScaleImg = source as AutoGreyableImage;
            bool isEnable = Convert.ToBoolean(args.NewValue);

            if (autoGreyScaleImg != null)
            {
                if (!isEnable)
                {
                    // Get the source bitmap
                    BitmapImage bitmapImage = new BitmapImage(new Uri(autoGreyScaleImg.Source.ToString()));

                    // Convert it to Gray
                    autoGreyScaleImg.Source = new FormatConvertedBitmap(bitmapImage, PixelFormats.Gray32Float, null, 0);

                    // Create Opacity Mask for greyscale image as FormatConvertedBitmap does not keep transparency info
                    autoGreyScaleImg.OpacityMask = new ImageBrush(bitmapImage);

                    autoGreyScaleImg.Opacity = 0.5;
                }
                else
                {
                    // Set the Source property to the original value.
                    autoGreyScaleImg.Source = ((FormatConvertedBitmap)autoGreyScaleImg.Source).Source;

                    // Reset the Opcity Mask
                    autoGreyScaleImg.OpacityMask = null;

                    autoGreyScaleImg.Opacity = 1;
                }
            }
        }
    }
}
