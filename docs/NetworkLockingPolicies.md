## Freecraft Core Message Handling Policies

There are two strategies for handling messages used. The first is usually used by simple services or the client and the second for others:

1. Simple single-threaded message queue polling.

2. Concurrent message handling (with lock policies).

## Concurrency and Locking Policies

**Five main types of locking policies**:

1. Default lock* 
2. Handler lock
3. Protocol lock
4. Global service lock
5. Custom**

\* The default is a policy makes the promise that for a given client their JAM messages will never be handled concurrently or out of order.

\*\*They also have custom policies that can "inherit" the policy in the main types but also extend the idea with modern read/writing locking for better throughput. For example, for a given protocol lock you may only be read locking for some messages in a given protocol and only fully write lock on some operation codes within the protocol.
