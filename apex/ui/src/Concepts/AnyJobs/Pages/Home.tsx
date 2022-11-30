import { Stack } from '@fluentui/react';
import './Home.css';
const Home = () => {
  return (
    <>
      <Stack verticalAlign='center' >
        <div id="wrap">
          <form action="" autoComplete="on">
            <input id="search" name="search" type="text" placeholder="What're we looking for ?" />
            <input id="search_submit" value="ReSearcher" type="submit" />
          </form>
        </div>
      </Stack>

    </>
  )
}

export default Home