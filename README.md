# DotNet Image Resizer
Image Resizing tool targeting .NET Standard 2.0

I have found myself constantly having to resize images in my personal projects and decided to finally make myself a re-usable package instead of looking up the code over and over. Currently supports three resize modes:
- **Cover**
	Resizes an image while maintaining aspect ratio, to completely cover the specified dimensions.
	
- **Contain**
	Resizes an image while maintaining aspect ratio, to fit completely within the specified dimensions
	
- **Stretch**
	Resizes an image with no regard for life or limb, forcing it to the exact dimensions specified
