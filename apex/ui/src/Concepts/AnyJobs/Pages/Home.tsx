import { getTheme, ILabelStyles, ISearchBoxStyles, Label, SearchBox } from '@fluentui/react';
import { useState } from 'react';
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
  let theme = getTheme();
  const searchBoxStyles: ISearchBoxStyles = {
    root: {
        fontSize: 'calc(10px + 2vw)',
        minHeight: '7vw',
        width:'75vw',
        color:theme.palette.themePrimary,
        
    },
    icon:
    {
      fontSize:'calc(10px + 3vw)', 
    },
    field:{
      paddingLeft:'4vw',   
      color:theme.palette.themePrimary,
      selectors: {
        ':placeholder-shown': {
        },
        ':focus': {
          paddingLeft:'0'
        }
      } 
    }
};
const labelStyles: ILabelStyles = {
  root: {
      fontSize:'calc(10px + 2vw)',
      paddingLeft:'1vw',
      overflow:'hidden',
      width:'75vw',
      selectors: {
        ':hover': {
          boxShadow: theme.effects.elevation4,
          color:theme.palette.themePrimary
        }
      }
  },
}
  return (
    <>
    <div className="search-component">
    <div>
      <SearchBox placeholder="What job are you looking for?" underlined={true} autoFocus
          onClear={()=>setValue('')}
            onSearch={(newValue:any)=>navigate('/anyjobs/jobs/search/'+newValue )} 
            onChange={onChange} value={value}
            styles={searchBoxStyles}></SearchBox>
        
          <div  >
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
                <Label
                  onClick={() => onSearch(item.Title)}
                  key={item.Title} styles={labelStyles}
                >
                  {item.Title}
                </Label>
              ))}
          </div>
        </div>
      </div>
    </>
  )
}

export default Home