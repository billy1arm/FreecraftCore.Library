# What is the Freecraft Core Pipeline System?

The Freecraft Core pipeline system is a varation of the [Chain of Responsbility](www.todo.url) pattern that allows 
handlers, or [Pipeline Components](www.todo.url), to process incoming [Command](www.todo.url) objects potentially augmenting them or indicating results.
The Command object in the case of Freecraft Core's pipeline system are called [Context](www.todo.url) which are themselves a complex Command pattern variation.
All pipeline's derive from the same root generic pipeline that takes in type parameters for the TResultType, TInputType and TStateType. This allows implementers
control of the data or result the pipeline component yields and provides state or input which can be either the context or a reader/writer component.

## Network Pipeline

For example, the network pipelines for incoming messages process on a Context containing an abstracted Stream behind the [FreecraftCore.Serialize.Stream Reader](https://github.com/FreecraftCore/FreecraftCore.Serializer/blob/master/src/FreecraftCore.Serializer.Stream/Read/IWireMemberReaderStrategy.cs)
and it allows modification and decoration of that reader object to for specific functionality.

## API Example

			authenticationIncomingPipelineService
				.WithPeekStream(o => o.For<PayloadPipeline.Main>().For<HeaderPipeline.Main>().WithOrder<OnTop>())
				.WithHeaderDeserializationPipeline<AuthenticationPacketHeader>(serializer)
				.WithOpCodeReinsertion(serializer, o => o.For<PayloadPipeline.Main>())
				.WithAuthDestinationCodeInsertion(serializer, AuthOperationDestinationCode.Client, o => o.For<PayloadPipeline.Main>())
				.WithPayloadDeserializationPipeline<AuthenticationPayload>(serializer);
        
#### Explaination
**WithPeakStram**: Pipeline component that adds the ability to peek/seek the incoming stream of data. This would otherwise not be possible on Sockets or NetworkStreams.

The lambda expression it takes is of type Func\<BaseOptions, ICompleteOptions\> (not exact typing) which can configure where and how this pipeline component is added.


**For\<TPipelineType\>**: Is an option extension that is able to register a particular pipeline for a pipeline type. For example PayloadPipeline is the pipeline type responsible
for building the Payload. PaloadPipeline.Main is a nested class type that registers it for the Main Payload pipeline. The API allows for error pipelines too.

**WithOrder\<TOrderType\>**: Is an option extension that is able to indicate how or where the pipeline component should be inserted. There are about three ordering strategies for OnTop or OnBottom available.
