using Amazon.XRay.Recorder.Core;
using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AwsConceptsRootLambda
{
    public class XrayInstrumentation: IApplicationLogger
    {

        public void AddAnnotation(string key, object value)
        {
            AWSXRayRecorder.Instance.AddAnnotation(key, value);
        }

        public void AddMetadata(string key, object value)
        {
            AWSXRayRecorder.Instance.AddMetadata(key, value);
        }

        public void AddException(Exception ex)
        {
            AWSXRayRecorder.Instance.AddException(ex);
        }
    }
}
