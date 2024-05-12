<h1>Orders Microservice</h1>

- How to handle service bus messages that have multiple subscribers but a few have failed
  - Add retry mechanisms.
  - Make sure endpoints are idempotent.
    - keep track of operations via db
  - Use orchestration to put the system back into its original state.