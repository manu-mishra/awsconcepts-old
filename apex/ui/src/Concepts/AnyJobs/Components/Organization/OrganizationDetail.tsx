import React from 'react'
import { useState } from 'react'
import { useParams } from "react-router";
import { API } from 'aws-amplify';
import { Organization } from '../../Model/OrganizationsModel';

export default function OrganizationDetail () {
  const { id } = useParams();
  const [organization, setOrganization] = useState<Organization>();
  React.useEffect(() => {
    if (id !== undefined) {
      const callApi = async () => {
        let respProfile = await API.get('api', '/Organizations/' + id, {
          responseType: 'json'
        }) as Organization;
        setOrganization(respProfile);
      }
      callApi().catch(console.error);
    }
  }, [id]);

  function createMarkup(markup:string) { return {__html: markup}; };
  if(organization)
  {
    return (
      <>
      <h1>{organization.name}</h1>
      <div dangerouslySetInnerHTML={createMarkup(organization.details)}></div>
      </>
    )
  }
  return (
    <div>loading</div>
  )
}
