# Blitter How-To
## Image Blitter
Attach the script onto the main camera in the scene.
</br>Put your images in the "Images" array of the script and check the "Show Image".
</br>Button will cycle through the array of images you have added.</br>
![image](https://github.com/yanchn-lim/Post-Process-Effects/assets/105861890/e219ee17-2b50-468a-a51e-cbf56cf4d778)

## Post Process Applicator
Attach the script onto the main camera **AFTER** the "Image Blitter" script.
</br>Simply tick the "Apply FX" to apply post processing effects.
</br>Create an empty gameobject with the **EXACT** name "PostProcessFX".
</br>Any effects that you want to apply would go under "PostProcessFX" gameobject as it's child. </br>
![image](https://github.com/yanchn-lim/Post-Process-Effects/assets/105861890/bd6d782d-29fc-4f66-8f57-43e9cd960fda)
![image](https://github.com/yanchn-lim/Post-Process-Effects/assets/105861890/42ad812c-9f2a-4b1b-a585-de756678be48)

## Post Process FX
Create a new script and inherit "PostProcessFX"
</br>Override the "ApplyShaderArguments" to apply your shader parameters.
</br>All methods can be overwritten to customize to your liking.
</br>Attach the script created into a gameobject and place it under the "PostProcessFX" gameobject.
</br>Add all the required variables into the script.</br>
![image](https://github.com/yanchn-lim/Post-Process-Effects/assets/105861890/537e369b-4885-4676-936a-df31ee9b29e6)</br>
![image](https://github.com/yanchn-lim/Post-Process-Effects/assets/105861890/bc9a9e1b-2cf6-4054-a3b6-83bff8e589d7)
![image](https://github.com/yanchn-lim/Post-Process-Effects/assets/105861890/459f39fd-3869-4eb9-a853-027e2130dcf6)

