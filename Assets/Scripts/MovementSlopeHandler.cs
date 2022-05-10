using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSlopeHandler : MonoBehaviour
{

    [SerializeField] Transform _checkPos;
    [SerializeField] float _checkDistance;
    [SerializeField] LayerMask _checkLayer;
    [SerializeField]float _slopeDownAngle;
     [SerializeField]float _slopeDownAngleOld;
    [SerializeField] float _slopeSideAngle;
    Vector2 _slopeNormalPerp;
    [SerializeField] bool _onSlope;
    [SerializeField] float _maxSlopeAngle;
    private void FixedUpdate()
    {
        SlopeCheck();
    }

    public bool OnSlope{
        get{
            return _onSlope;
        }
    }
    public Vector2 SlopeNormalPerp{
        get{
            return -_slopeNormalPerp;
        }
    }
    private void SlopeCheck(){
        CheckVertical(_checkPos.position,Vector2.down);
        CheckHorizontal();
    }

    private void CheckVertical(Vector3 pos,Vector2 dir){
        RaycastHit2D hit = Physics2D.Raycast(_checkPos.position,-_checkPos.up,_checkDistance,_checkLayer);
        if(hit == false)return;
            _slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;
            _slopeDownAngle = Vector2.Angle(hit.normal,Vector2.up);
            if(_slopeDownAngle != _slopeDownAngleOld)_onSlope = true;
            _slopeDownAngleOld =_slopeDownAngle;
            Debug.DrawRay(hit.point,hit.normal,Color.green);
            Debug.DrawRay(hit.point,_slopeNormalPerp,Color.red);
        
    }

    private void CheckHorizontal(){
        RaycastHit2D hitRight = Physics2D.Raycast(_checkPos.position,_checkPos.right,_checkDistance,_checkLayer);
        RaycastHit2D hitLeft = Physics2D.Raycast(_checkPos.position,-_checkPos.right,_checkDistance,_checkLayer);
        if(hitLeft == false && hitRight == false){
            _slopeSideAngle = 0;
            _onSlope = false;
            return;
        }
        if(hitLeft){
            _slopeSideAngle = Vector2.Angle(hitLeft.normal,Vector2.up);
            _onSlope = true;
        }
        if(hitRight){
            _slopeSideAngle = Vector2.Angle(hitRight.normal,Vector2.up);
            _onSlope = true;
        }
        if(_slopeSideAngle  == 90)_onSlope = false;
    }
}
