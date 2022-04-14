using System.Collections;
using CreatingCharacters.Player;
using System.Collections.Generic;
using UnityEngine;

namespace CreatingCharacters.Abilities
{
    public class RewindTime : MonoBehaviour
    {
        //The higher maxRecallData is, the smoother the rewind is (2 is straight line, 100 is a line of 100 points making the rewind smoother)
        [SerializeField] private int maxRecallData = 6;
        //This maxRecallData / secondsBetweenData
        [SerializeField] private float secondsBetweenData = 0.5f;
        // recallDuration represents the seconds back in time that the character goes
        [SerializeField] private float recallDuration = 1.25f;

        private PlayerCameraController playerCameraController;  // PlayerCameraController
        private bool canCollectRecallData = true;
        private float currentDataTimer = 0f;

        [System.Serializable]
        private class RecallData
        {
            public Vector3 characterPosition;
            public Quaternion characterRotation;
            public Quaternion cameraRotation;
        }

        [SerializeField] private List<RecallData> recallData = new List<RecallData>();

        private void Start()
        {
            playerCameraController = GetComponentInChildren<PlayerCameraController>(); //playerCameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MoveCamera>(); 
        }

        private void Update()
        {
            StoreRecallData();

            //Remove this, just for debugging purposes
            for(int i = 0; i<recallData.Count -1;i++)
            {
                Debug.DrawLine(recallData[i].characterPosition, recallData[i+1].characterPosition);
            }

            RecallInput();
        }

        private void RecallInput()
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
               StartCoroutine(Recall());
            }
        }
        private void StoreRecallData()
        {
            currentDataTimer +=Time.deltaTime;

            if(canCollectRecallData)
            {
                if(currentDataTimer >= secondsBetweenData)
                {
                    if(recallData.Count >= maxRecallData)
                    {
                        recallData.RemoveAt(0);
                    }
                    recallData.Add(GetRecallData());

                    currentDataTimer = 0f;
                }
            }
        }

        private RecallData GetRecallData()
        {
            return new RecallData()
            {
                characterPosition = transform.position,
                characterRotation = transform.rotation, 
                cameraRotation = playerCameraController.transform.rotation
            };
        }
    
        private IEnumerator Recall()
        {
            playerCameraController.Lock(true);

            canCollectRecallData = false;

            float secondsForEachData = recallDuration / recallData.Count;

            Vector3 currentDataPlayerStartPos = transform.position;
            Quaternion  currentDataPlayerStartRot = transform.rotation;
            Quaternion currentDataCamStartRot = playerCameraController.transform.rotation;
            Vector3 currentDataCamStartPos = playerCameraController.transform.position;


            while(recallData.Count > 0)
            {
                float t = 0f;

                while( t < secondsForEachData ){

                   transform.position = Vector3.Lerp(currentDataPlayerStartPos,
                    recallData[recallData.Count -1].characterPosition,
                     t/secondsForEachData);

                     transform.rotation = Quaternion.Lerp(currentDataPlayerStartRot,
                      recallData[recallData.Count -1].characterRotation,
                       t/secondsForEachData);

                       playerCameraController.transform.rotation = Quaternion.Lerp(currentDataCamStartRot,
                      recallData[recallData.Count -1].cameraRotation,
                       t/secondsForEachData);
                  
                    t += Time.deltaTime;

                    yield return null;
                }

                currentDataPlayerStartPos = recallData[recallData.Count -1].characterPosition;
                currentDataPlayerStartRot = recallData[recallData.Count -1].characterRotation;
                currentDataCamStartRot = recallData[recallData.Count -1].cameraRotation;
                currentDataCamStartPos = recallData[recallData.Count -1].characterPosition;


                recallData.RemoveAt(recallData.Count -1);
            }

            playerCameraController.Lock(false);

            canCollectRecallData = true;
        }
    } 

}
