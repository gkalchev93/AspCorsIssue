import logo from './logo.svg';
import './App.css';
import {
  ApolloClient,
  InMemoryCache,
  ApolloProvider,
  useQuery,
  gql
} from "@apollo/client";

const client = new ApolloClient({
  uri: 'https://localhost:7248/graphql',
  cache: new InMemoryCache()
})


const testCORS = () => {
  client
    .query({
      query: gql`
        query{
	        testData
        }
    `
    })
    .then(result => console.log(result));
};

testCORS();



function App() {
  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          HotChocolate Cors Problem Test
        </p>
      </header>
    </div>
  );
}

export default App;
