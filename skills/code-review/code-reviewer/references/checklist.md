# Code Review Checklist

## Correctness

- Validate control flow and boundary cases.
- Check null/empty/error handling.
- Verify behavior matches function intent.

## Security

- Validate user input and output encoding.
- Check query construction and command execution.
- Ensure secrets are not exposed.

## Maintainability

- Flag duplication and brittle logic.
- Check naming clarity and cohesion.
- Identify hidden coupling.

## Testing

- Confirm happy-path coverage.
- Confirm edge and failure-path coverage.
- Call out missing regression tests for changed behavior.
