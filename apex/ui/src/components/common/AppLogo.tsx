import { useNavigate } from 'react-router-dom';
export const AppLogo = () => {
  const navigate = useNavigate();
  return (
    <img src= '/awsconceptslogo.png' onClick={() => navigate('/')} alt ='aws concepts logo' style={{width:'10vw', paddingLeft:'1vw'}}></img>
  )
}
