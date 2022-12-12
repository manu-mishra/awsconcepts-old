import { Stack } from '@fluentui/react';
import { KeyboardEvent, useState } from 'react';
import { useNavigate } from 'react-router-dom';

import './Home.css';
var data = require("./mockdata.json");


const Home = () => {
  const [value, setValue] = useState("");

  const navigate = useNavigate();
  const onChange = (event:any) => {
    setValue(event.target.value);
  };

  const onSearch = (searchTerm:any) => {
    setValue(searchTerm);
    navigate('/anyjobs/jobs/search/'+searchTerm )
  };
  const handleKeyDown = (keyPress: KeyboardEvent) => {
    if (keyPress?.key === 'Enter') {
      navigate('/anyjobs/jobs/search/'+value )
    }
  };

  return (
    <>
    <div className="search-component">
      <Stack verticalAlign='center' verticalFill horizontalAlign='center'>
        <div>
          <div className="search-fields">
            <input autoFocus  type="text" value={value} onChange={onChange} onKeyUp={handleKeyDown} placeholder="What job are you looking for?"/>
          </div>
          <div >
            {data
              .filter((item:any) => {
                const searchTerm = value.toLowerCase();
                const Title = item.Title.toLowerCase();

                return (
                  searchTerm &&
                  Title.startsWith(searchTerm) &&
                  Title !== searchTerm
                );
              })
              .slice(0, 10)
              .map((item:any) => (
                <div
                  onClick={() => onSearch(item.Title)}
                  className="dropdown-row"
                  key={item.Title}
                >
                  {item.Title}
                </div>
              ))}
          </div>
        </div>
      </Stack>
      </div>
    </>
  )
}

export default Home