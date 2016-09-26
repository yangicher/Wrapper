/**
 * Copyright (C) 2005-2014 by Rivello Multimedia Consulting (RMC).                    
 * code [at] RivelloMultimediaConsulting [dot] com                                                  
 *                                                                      
 * Permission is hereby granted, free of charge, to any person obtaining
 * a copy of this software and associated documentation files (the      
 * "Software"), to deal in the Software without restriction, including  
 * without limitation the rights to use, copy, modify, merge, publish,  
 * distribute, sublicense, and#or sell copies of the Software, and to   
 * permit persons to whom the Software is furnished to do so, subject to
 * the following conditions:                                            
 *                                                                      
 * The above copyright notice and this permission notice shall be       
 * included in all copies or substantial portions of the Software.      
 *                                                                      
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,      
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF   
 * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
 * IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR    
 * OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
 * ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
 * OTHER DEALINGS IN THE SOFTWARE.                                      
 */
// Marks the right margin of code *******************************************************************


//--------------------------------------
//  Imports
//--------------------------------------
using UnityEngine;
using strange.extensions.mediation.impl;
using com.rmc.projects.paddle_soccer.mvcs.view.signals;
using com.rmc.utilities;
using com.rmc.projects.paddle_soccer.mvcs.model.vo;
using com.rmc.projects.animation_monitor;


//--------------------------------------
//  Namespace
//--------------------------------------
namespace com.rmc.projects.paddle_soccer.mvcs.view.ui
{
	
	//--------------------------------------
	//  Namespace Properties
	//--------------------------------------
	/// <summary>
	/// Intro mode.
	/// </summary>
	public enum IntroMode
	{
		Show,
		Skip
	}
	
	//--------------------------------------
	//  Class Attributes
	//--------------------------------------
	
	//--------------------------------------
	//  Class
	//--------------------------------------
	[RequireComponent (typeof(Animation), typeof(AnimationMonitor))]
	public class IntroUI : View 
	{
		
		//--------------------------------------
		//  Properties
		//--------------------------------------
		
		// GETTER / SETTER

		// PUBLIC
		
		/// <summary>
		/// When the user interface button clicked signal.
		/// </summary>
		public UIInputChangedSignal uiInputChangedSignal {set; get;}
		
		/// <summary>
		/// When the click GUI text_gameobject.
		/// </summary>
		public GameObject clickGUIText_gameobject;


		/// <summary>
		/// When the logo GUI texture_gameobjects.
		/// </summary>
		public GameObject logoGUITexture_gameobjects;
		
		//  PUBLIC STATIC
		/// <summary>
		/// When the ANIMATIO n_ NAM e_ INTROU i_ STAR.
		/// </summary>
		public const string ANIMATION_NAME_INTRO_UI_START 	= "IntroUI_Start";
		
		/// <summary>
		/// When the ANIMATIO n_ NAM e_ INTROU i_ STAR.
		/// </summary>
		public const string ANIMATION_NAME_INTRO_UI_END 	= "IntroUI_End";
		
		/// <summary>
		/// When the intro mode.
		/// </summary>
		public IntroMode introMode = IntroMode.Show;
		
		/// <summary>
		/// When the intro animation clip.
		/// </summary>
		public AnimationClip introStartAnimationClip;
		
		/// <summary>
		/// When the outro animation clip.
		/// </summary>
		public AnimationClip introEndAnimationClip;
		
		
		/// <summary>
		/// When the _animation binder.
		/// 
		/// NOTE: Notifies when an animation is complete
		/// 
		/// </summary>
		public AnimationMonitor animationMonitor;
		
		// PRIVATE STATIC
		
		//
		/// <summary>
		/// When the _click GUI text.
		/// </summary>
		private GUIText _clickGUIText;

		/// <summary>
		/// When the _logo_guitexture.
		/// </summary>
		private GUITexture _logo_guitexture;
		
		
		//--------------------------------------
		//  Methods
		//--------------------------------------	
		
		
		
		/// <summary>
		/// Init this instance.
		/// </summary>
		public void init ()
		{
			uiInputChangedSignal 	= new UIInputChangedSignal();
		}
		
		
		///<summary>
		///	Use this for initialization
		///</summary>
		override protected void Start () 
		{
			
			base.Start();


			//
			_clickGUIText 	= clickGUIText_gameobject.GetComponent<GUIText>();
			_logo_guitexture = logoGUITexture_gameobjects.GetComponent<GUITexture>();
			animationMonitor = GetComponent<AnimationMonitor>();

			//EXPERIMENT: STORE CLIPS IN CUSTOM PROPERTIES AND ADD THEM DYNAMICALLY
			//	NOT REQUIRED FOR THE CURRENT SETUP, BUT COULD BE USEFUL FOR DYNAMIC
			//	ANIMATION SETUP
			GetComponent<Animation>().AddClip (introStartAnimationClip, ANIMATION_NAME_INTRO_UI_START);
			GetComponent<Animation>().AddClip (introEndAnimationClip, ANIMATION_NAME_INTRO_UI_END);
			
			
			
		}
		
		
		///<summary>
		///	Called once per frame
		///</summary>
		void Update () 
		{
			
			if (Input.GetMouseButton(0) || Input.anyKey) {
				
				//SEND ANY KEYCODE. ITS NOT IMPORTANT
				uiInputChangedSignal.Dispatch (new  UIInputVO (KeyCode.A, UIInputEventType.DownEnter));
				
			}
			
		}
		
		/// <summary>
		/// Raises the destroy event.
		/// </summary>
		override protected void OnDestroy ()
		{
			//
			base.OnDestroy();
			
		}
		
		/// <summary>
		/// Sets the health text.
		/// </summary>
		/// <param name="aMessage_string">A message_string.</param>
		public void setClickText (string aMessage_string)
		{
			_clickGUIText.text = aMessage_string;
			
		}
		
		
		/// <summary>
		/// Sets the click text is visible.
		/// </summary>
		/// <param name="isVisible_boolean">Is visible_boolean.</param>
		public void setClickTextIsVisible (bool isVisible_boolean)
		{
			
			RendererUtility.SetMaterialVisibility (_clickGUIText.material, isVisible_boolean);
		}

		/// <summary>
		/// Sets the logo texture is visible.
		/// </summary>
		/// <param name="isVisible_boolean">If set to <c>true</c> is visible_boolean.</param>
		public void setLogoTextureIsVisible (bool isVisible_boolean)
		{

			RendererUtility.SetGUITextureVisibility (_logo_guitexture, isVisible_boolean);
		}

		
		/// <summary>
		/// Do the play animation.
		/// </summary>
		/// <param name="aAnimationType">A animation type.</param>
		/// <param name="aDelayBeforeAnimation_float">A delay before animation_float.</param>
		/// <param name="aDelayAfterAnimation_float">A delay after animation_float.</param>
		public void doPlayAnimation (string aAnimationClipName_string, float aDelayBeforeAnimation_float, float aDelayAfterAnimation_float) 
		{

			//
			doStopAnimation();


			//CAN SKIP VIA INSPECTOR WINDOW DROPDOWN
			if (introMode == IntroMode.Skip) {
				aAnimationClipName_string = ANIMATION_NAME_INTRO_UI_END;
			} else {
				//REFRESH IN CASE IntroMode changed DURING gameplay
				gameObject.SetActive (true);
			}
			animationMonitor.playRequest ( new AnimationMonitorRequestVO (aAnimationClipName_string, WrapMode.Default, aDelayBeforeAnimation_float, aDelayAfterAnimation_float));

			
		}

		/// <summary>
		/// Do the stop animation.
		/// </summary>
		public void doStopAnimation()
		{
			GetComponent<Animation>().Stop();
			
			
		}

		
		// PUBLIC STATIC
		
		// PRIVATE
		
		// PRIVATE STATIC
		
		// PRIVATE COROUTINE
		
		// PRIVATE INVOKE
		
		//--------------------------------------
		//  Events 
		//		(This is a loose term for -- handling incoming messaging)
		//
		//--------------------------------------

		
	}
}

