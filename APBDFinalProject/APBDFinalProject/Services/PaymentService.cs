
using APBDFinalProject.Exceptions;
using APBDFinalProject.Models;
using APBDFinalProject.Repositories;
using APBDFinalProject.RequestModels;
using APBDFinalProject.Services.Abstractions;

namespace APBDFinalProject.Services;

public class PaymentService : IPaymentService
{
    private IPaymentRepository _paymentRepository;
    private IContractRepository _contractRepository;

    public PaymentService(IPaymentRepository paymentRepository, IContractRepository contractRepository)
    {
        _paymentRepository = paymentRepository;
        _contractRepository = contractRepository;
    }

    

    public async Task MakePayment(PaymentRequest paymentRequest, CancellationToken cancellationToken)
    {
        Contract contract = (await ContractWithGivenDetailsExists(paymentRequest.IdContract, paymentRequest.IdCustomer,
            cancellationToken))!;
        GivenContractAlreadyPaid(contract);
        PaymentDeadlinePassed(contract.StartDate, contract.DaysSpan);
        decimal paidAmount = PaymentExceedsTotalPrice(contract.TotalContractPrice, contract.AmountPaid,
            paymentRequest.AmountToPay);
        await _contractRepository.UpdateContractAmountPaid(contract, paidAmount, cancellationToken);
        if (paidAmount == contract.TotalContractPrice)
        {
            await _contractRepository.MakeContractPaid(contract, cancellationToken);
        }

        await _paymentRepository.AddPayment(new Payment()
        {
            IdContract = paymentRequest.IdContract,
            IdCustomer = paymentRequest.IdCustomer,
            AmountPaid = paymentRequest.AmountToPay,
            PaymentDate = DateTime.Now
        });
        
    }

    private async Task<Contract?> ContractWithGivenDetailsExists(int contractId, int customerId, CancellationToken cancellationToken)
    {
        Contract? contract =
            await _contractRepository.GetContractByIdAndCustomerId(contractId, customerId, cancellationToken);
        if (contract == null)
            throw new DomainException("Contract with given id and customer id was not found");

        return contract;
    }

    private void GivenContractAlreadyPaid(Contract contract)
    {
        if (contract.IsPaid)
            throw new DomainException("Contract was already paid, no need to make another payment");
    }

    private void PaymentDeadlinePassed(DateTime startDate, int daysSpan)
    {
        var paymentDeadline = startDate.AddDays(daysSpan);
        if (DateTime.Now < startDate || DateTime.Now > paymentDeadline)
        {
            throw new InvalidOperationException("You can't pay for this contract because the time of payment has already passed.");
        }
    }

    private decimal PaymentExceedsTotalPrice(decimal totalPrice, decimal amountPaid, decimal payAmount)
    {
        var paidAmount = amountPaid + payAmount;
        if (paidAmount > totalPrice)
            throw new DomainException("You can't pay more than the contract itself.");
        return paidAmount;
    }
}