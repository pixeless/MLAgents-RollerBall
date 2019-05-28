﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class RollerAgent2 : Agent
{
    Rigidbody rBody;
    public Transform Target;
    public float Speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if( Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if( Physics.Raycast(ray, out hit))
            {
                GameObject colliderObj = hit.collider.gameObject;
                if(colliderObj != gameObject && colliderObj != Target.gameObject)
                {
                    Target.position = new Vector3(hit.point.x, 0.5f, hit.point.z);
                }
            }
        }
    }

    public override void AgentReset()
    {
        if( this.transform.position.y < 0.0f )
        {
            // Reset Roller Ball Positon
            this.rBody.angularVelocity = Vector3.zero;
            this.rBody.velocity = Vector3.zero;

            /*
            int offsetRoller = Random.value > 0.5f ? 1 : -1;
            this.transform.position = new Vector3(0, 0.5f, offsetRoller * 2);
            */
            this.transform.position = new Vector3(0, 0.5f, 0);
        }
        // Reset Target Position
        int offset = Random.value > 0.5f ? 1 : -1;
        Target.position = new Vector3(Random.value * 10 - 5 + 10 * offset, 0.5f, Random.value * 10 - 5);
    }

    public override void CollectObservations()
    {
        AddVectorObs(Target.position);
        AddVectorObs(this.transform.position);

        AddVectorObs(rBody.velocity.x);
        AddVectorObs(rBody.velocity.z);
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = vectorAction[0];
        controlSignal.z = vectorAction[1];
        controlSignal.Normalize();
        rBody.AddForce(controlSignal * Speed);

        // Rewards
        //AddReward(-0.001f);
        float distanceTotarget = Vector3.Distance(this.transform.position, Target.position);

        // Reached target
        if( distanceTotarget < 1.42f)
        {
            SetReward(1.0f);
            Done();
        }
        // Fell off platform
        if( this.transform.position.y < 0)
        {
            SetReward(-1.0f);
            Done();
        }

        if( this.GetStepCount() > 12768 )
        {
            SetReward(-1.0f);
            Done();
        }
    }

}
