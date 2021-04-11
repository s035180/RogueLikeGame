using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugReport : MonoBehaviour
{
    // Start is called before the first frame update
    public void openBugReport()
    {
        // Create a process
        System.Diagnostics.Process process = new System.Diagnostics.Process();

        // Set the StartInfo of process
        process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
        process.StartInfo.FileName = @".\BugReport\BugReport.exe";
        process.StartInfo.Arguments = @"/c -sk server -sky exchange -pe -n CN=localhost -ir LocalMachine -is Root -ic MyCA.cer -sr LocalMachine -ss My MyAdHocTestCert.cer";

        // Start the process
        process.Start();
        process.WaitForExit();
    }
}
