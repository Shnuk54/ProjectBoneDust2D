using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorState : MonoBehaviour
{
  private Animator _animator;
  private Dictionary<string,AnimationState> _animations = new Dictionary<string, AnimationState>();
  private string _currentLegsAnim = string.Empty;
  private string _currentArmsAnim = string.Empty;
  private string _currentBodyAnim = string.Empty;

  void Awake() {
      _animator = this.GetComponent<Animator>();
  }
    void Update() {
    Animate();
        }

    public void AddAnimations(params AnimationState[] anims){
        foreach(AnimationState anim in anims){
            this._animations.Add(anim.animName,anim);
            Debug.Log(anim.animName);
        }
    }

    public void PlayAnimation(string newAnimName){
        if(_animations[newAnimName].animPart == AnimationPart.Body){
            playAnimation(ref _currentBodyAnim);
        }
        if(_animations[newAnimName].animPart == AnimationPart.Legs){
            playAnimation(ref _currentLegsAnim);
        }
        if(_animations[newAnimName].animPart == AnimationPart.Arms){
            playAnimation(ref _currentArmsAnim);
        }

        void playAnimation(ref string currentAnimation){
            if(currentAnimation == ""){
                _animations[newAnimName].isActive = true;
                currentAnimation = newAnimName;
            }
            if(currentAnimation != newAnimName){
                 _animations[currentAnimation].isActive = false;
                  _animations[newAnimName].isActive = true;
                currentAnimation = newAnimName;
            }
        }
    }

    private void Animate(){
        foreach (string name in _animations.Keys)
        {
            _animator.SetBool(name,_animations[name].isActive);
        }
    }
}
