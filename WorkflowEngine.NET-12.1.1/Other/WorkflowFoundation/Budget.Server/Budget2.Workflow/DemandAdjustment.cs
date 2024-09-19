﻿using System;
using System.Collections.Generic;
using System.Workflow.Activities;
using Budget2.DAL.DataContracts;

namespace Budget2.Workflow
{
    public sealed partial class DemandAdjustment : BudgetWorkflow
    {
        public new Dictionary<string, object> WorkflowPersistanceParameters
        {
            set
            {
                base.WorkflowPersistanceParameters = value;
            }
            get
            {
                return base.WorkflowPersistanceParameters;
            }

        }

        public DemandAdjustment()
        {
            InitializeComponent();
        }

        private void CheckLimitIsContingency_ExecuteCode(object sender, ConditionalEventArgs e)
        {
            e.Result = Budget2WorkflowRuntime.DemandAdjustmentBusinessService.CheckLimitIsContingency(WorkflowInstanceId);
        }

        private void CheckLimitsManagersIsSame_ExecuteCode(object sender, ConditionalEventArgs e)
        {
            e.Result = Budget2WorkflowRuntime.DemandAdjustmentBusinessService.CheckLimitsManagersIsSame(WorkflowInstanceId);
        }

        private void CheckLimitDistributionIsFromFutureQuoterToCurrent_ExecuteCode(object sender, ConditionalEventArgs e)
        {
            e.Result = Budget2WorkflowRuntime.DemandAdjustmentBusinessService.CheckToSkipUPKZSighting(WorkflowInstanceId);
        }

        private void CheckToSkipSourceDemandLimitManagerSighting_ExecuteCode(object sender, ConditionalEventArgs e)
        {
            e.Result = Budget2WorkflowRuntime.DemandAdjustmentBusinessService.CheckToSkipSourceDemandLimitManagerSighting(WorkflowInstanceId);
        }

        private void CheckToSkipTargetDemandLimitManagerSighting_ExecuteCode(object sender, ConditionalEventArgs e)
        {
            e.Result = Budget2WorkflowRuntime.DemandAdjustmentBusinessService.CheckToSkipTargetDemandLimitManagerSighting(WorkflowInstanceId);
        }

        private void CheckToSkipTargetDemandLimitExecutorSighting_ExecuteCode(object sender, ConditionalEventArgs e)
        {
            e.Result = Budget2WorkflowRuntime.DemandAdjustmentBusinessService.CheckToSkipTargetDemandLimitExecutorSighting(WorkflowInstanceId);
        }

        private void Check_DemanAdjustmentTypeIsNotRedistrubution(object sender, ConditionalEventArgs e)
        {
            e.Result = Budget2WorkflowRuntime.DemandAdjustmentBusinessService.GetDemandAdjustmentType(WorkflowInstanceId) != DemandAdjustmentType.Redistribution;
            if (e.Result)
                LastCommand = WorkflowCommand.Sighting;
        }

        private void DraftInitCode_ExecuteCode(object sender, EventArgs e)
        {
            WriteTransitionToHistory(WorkflowState.DemandAdjustmentDraft);
            PreviousWorkflowState = WorkflowState.DemandAdjustmentDraft;
        }

        private void SourceDemandLimitExecutorSightingInitCode_ExecuteCode(object sender, EventArgs e)
        {
            WriteTransitionToHistory(WorkflowState.DemandAdjustmentSourceDemandLimitExecutorSighting);
            PreviousWorkflowState = WorkflowState.DemandAdjustmentSourceDemandLimitExecutorSighting;
        }

        private void SourceDemandLimitManagerSightingInitCode_ExecuteCode(object sender, EventArgs e)
        {
            WriteTransitionToHistory(WorkflowState.DemandAdjustmentSourceDemandLimitManagerSighting);
            PreviousWorkflowState = WorkflowState.DemandAdjustmentSourceDemandLimitManagerSighting;
        }

        private void AgreedInitCode_ExecuteCode(object sender, EventArgs e)
        {
            WriteTransitionToHistory(WorkflowState.DemandAdjustmentAgreed);
            PreviousWorkflowState = WorkflowState.DemandAdjustmentAgreed;
            Budget2WorkflowRuntime.DemandAdjustmentBusinessService.SetAgreedDate(WorkflowInstanceId);
        }

        private void TargetDemandLimitExecutorSightingInitCode_ExecuteCode(object sender, EventArgs e)
        {
            WriteTransitionToHistory(WorkflowState.DemandAdjustmentTargetDemandLimitExecutorSighting);
            PreviousWorkflowState = WorkflowState.DemandAdjustmentTargetDemandLimitExecutorSighting;
        }

        private void UPKZCuratorSightingInitCode_ExecuteCode(object sender, EventArgs e)
        {
            WriteTransitionToHistory(WorkflowState.DemandAdjustmentUPKZCuratorSighting);
            PreviousWorkflowState = WorkflowState.DemandAdjustmentUPKZCuratorSighting;
        }

        #region Вспомогательные функции


        private void WriteTransitionToHistory(WorkflowState current)
        {
            if (DontWriteToWorkflowHistory)
                return;

            SetStateIfParcelExists();

            if (PreviousWorkflowState == null || PreviousWorkflowState.WorkflowStateName == WorkflowState.DemandAdjustmentDraft.WorkflowStateName)
            {
                Budget2WorkflowRuntime.DemandAdjustmentBusinessService.CreateDemandAdjustmentPreHistory(WorkflowInstanceId, WorkflowState.DemandAdjustmentDraft);
            }

            if (PreviousWorkflowState == null)
            {
                Budget2WorkflowRuntime.DemandAdjustmentBusinessService.UpdateDemandAdjustmentState(current, WorkflowInstanceId);
                return;
            }


            Budget2WorkflowRuntime.DemandAdjustmentBusinessService.UpdateDemandAdjustmentState(PreviousWorkflowState, current, LastCommand,
                                                                                  WorkflowInstanceId,
                                                                                  TransitionInitiator, Comment);

            Comment = string.Empty;
        }

        #endregion

        private void UPKZHeadSightingInitCode_ExecuteCode(object sender, EventArgs e)
        {
            WriteTransitionToHistory(WorkflowState.DemandAdjustmentUPKZHeadSighting);
            PreviousWorkflowState = WorkflowState.DemandAdjustmentUPKZHeadSighting;
        }

        private void TargetDemandLimitManagerSightingInitCode_ExecuteCode(object sender, EventArgs e)
        {
            WriteTransitionToHistory(WorkflowState.DemandAdjustmentTargetDemandLimitManagerSighting);
            PreviousWorkflowState = WorkflowState.DemandAdjustmentTargetDemandLimitManagerSighting;
        }

        private void TargetDemandLimitExecutorSightingInitCode1_ExecuteCode(object sender, EventArgs e)
        {

            WriteTransitionToHistory(WorkflowState.DemandAdjustmentTargetDemandLimitExecutorSighting);
            PreviousWorkflowState = WorkflowState.DemandAdjustmentTargetDemandLimitExecutorSighting;

            var sightersIds = Budget2WorkflowRuntime.DemandAdjustmentBusinessService.GetSightersId(WorkflowInstanceId);

            TransitionInitiator = sightersIds.SourceDemandLimitExecutor.HasValue ? sightersIds.SourceDemandLimitExecutor.Value : Guid.Empty;

            WriteTransitionToHistory(WorkflowState.DemandAdjustmentTargetDemandLimitManagerSighting);
            PreviousWorkflowState = WorkflowState.DemandAdjustmentTargetDemandLimitManagerSighting;


            TransitionInitiator = sightersIds.SourceDemandLimitManager.HasValue ? sightersIds.SourceDemandLimitManager.Value : Guid.Empty;

        }

        private void StartProcessingEventInvoked(object sender, ExternalDataEventArgs e)
        {
            forvadTransitionEventInvoked(sender, e);
            Budget2WorkflowRuntime.DemandAdjustmentBusinessService.SetStartProcessingDate(WorkflowInstanceId);
            LastCommand = WorkflowCommand.StartProcessing;
        }
    }
}
